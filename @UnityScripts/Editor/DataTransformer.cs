//using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

// 인프런 [Rookiss] 켠킴에 출시까지 강의, 참고

// 1. Unity의 $"{Application.dataPath}/@Resources/Data/ExcelData/{filename}Data.csv" 자리에 있는 csv 파일을 가져와서 Json 형태로 변환한 후
// 2. $"{Application.dataPath}/@Resources/Data/JsonData/{filename}Data.json" 경로에 생성해주는 스크립트


public class DataTransformer : EditorWindow
{
#if UNITY_EDITOR
    [MenuItem("Tools/ParseExcel %#K")]
    public static void ParseExcelDataToJson()
    {
        //ParseExcelDataToJson<TestDataLoader, TestData>("Test");
        //LEGACY_ParseTestData("Test");

        Debug.Log("DataTransformer Completed");
    }

    #region LEGACY
    // LEGACY !
    public static T ConvertValue<T>(string value)
    {
        if (string.IsNullOrEmpty(value))
            return default(T);

        TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
        return (T)converter.ConvertFromString(value);
    }

    public static List<T> ConvertList<T>(string value)
    {
        if (string.IsNullOrEmpty(value))
            return new List<T>();

        return value.Split('&').Select(x => ConvertValue<T>(x)).ToList();


    }

    //static void LEGACY_ParseTestData(string filename)
    //{
    //    TestDataLoader loader = new TestDataLoader();

    //    string[] lines = File.ReadAllText($"{Application.dataPath}/@Resources/Data/ExcelData/{filename}Data.csv").Split("\n");

    //    for (int y = 1; y < lines.Length; y++)
    //    {
    //        string[] row = lines[y].Replace("\r", "").Split(',');
    //        if (row.Length == 0)
    //            continue;
    //        if (string.IsNullOrEmpty(row[0]))
    //            continue;

    //        int i = 0;
    //        TestData testData = new TestData();
    //        testData.Level = ConvertValue<int>(row[i++]);
    //        testData.Exp = ConvertValue<int>(row[i++]);
    //        testData.Skills = ConvertList<int>(row[i++]);
    //        testData.Speed = ConvertValue<float>(row[i++]);
    //        testData.Name = ConvertValue<string>(row[i++]);

    //        loader.tests.Add(testData);
    //    }

    //    string jsonStr = JsonConvert.SerializeObject(loader, Formatting.Indented);
    //    File.WriteAllText($"{Application.dataPath}/@Resources/Data/JsonData/{filename}Data.json", jsonStr);
    //    AssetDatabase.Refresh();
    //}
    #endregion

    #region Helpers
    private static void ParseExcelDataToJson<Loader, LoaderData>(string filename) where Loader : new() where LoaderData : new()
    {
        Loader loader = new Loader();
        FieldInfo field = loader.GetType().GetFields()[0];
        field.SetValue(loader, ParseExcelDataToList<LoaderData>(filename));

        string jsonStr = JsonConvert.SerializeObject(loader, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/@Resources/Data/JsonData/{filename}Data.json", jsonStr);
        AssetDatabase.Refresh();
    }

    private static List<LoaderData> ParseExcelDataToList<LoaderData>(string filename) where LoaderData : new()
    {
        List<LoaderData> loaderDatas = new List<LoaderData>();

        string[] lines = File.ReadAllText($"{Application.dataPath}/@Resources/Data/ExcelData/{filename}Data.csv").Split("\n");

        for (int l = 1; l < lines.Length; l++)
        {
            string[] row = lines[l].Replace("\r", "").Split(',');
            if (row.Length == 0)
                continue;
            if (string.IsNullOrEmpty(row[0]))
                continue;

            LoaderData loaderData = new LoaderData();

            System.Reflection.FieldInfo[] fields = typeof(LoaderData).GetFields();
            for (int f = 0; f < fields.Length; f++)
            {
                FieldInfo field = loaderData.GetType().GetField(fields[f].Name);
                Type type = field.FieldType;

                if (type.IsGenericType)
                {
                    object value = ConvertList(row[f], type);
                    field.SetValue(loaderData, value);
                }
                else
                {
                    object value = ConvertValue(row[f], field.FieldType);
                    field.SetValue(loaderData, value);
                }
            }

            loaderDatas.Add(loaderData);
        }

        return loaderDatas;
    }

    private static object ConvertValue(string value, Type type)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        TypeConverter converter = TypeDescriptor.GetConverter(type);
        return converter.ConvertFromString(value);
    }

    private static object ConvertList(string value, Type type)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        // Reflection
        Type valueType = type.GetGenericArguments()[0];
        Type genericListType = typeof(List<>).MakeGenericType(valueType);
        var genericList = Activator.CreateInstance(genericListType) as IList;

        // Parse Excel
        var list = value.Split('&').Select(x => ConvertValue(x, valueType)).ToList();

        foreach (var item in list)
            genericList.Add(item);

        return genericList;
    }
    #endregion

#endif
}