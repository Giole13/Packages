#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;


// 참고 : https://kkk159147.tistory.com/6
// 씬 이동을 단축키로 좀더 편하게 이동할 수 있도록 도와주는 에디터 스크립트
public class UnitySceneChangerEditor : Editor
{
    const string SCENE_PATH = "Assets/@Scenes/";

    // 현재 씬 저장, 해당 경로 씬 파일 오픈
    private static void LoadScene(string sceneName)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        EditorSceneManager.SaveScene(currentScene);
        EditorSceneManager.OpenScene(SCENE_PATH + sceneName);
    }

    [MenuItem("Tools/SceneChange/TitleScene &1")]
    private static void TitleScene() => LoadScene("TitleScene.unity");

    [MenuItem("Tools/SceneChange/GameScene &2")]
    private static void GameScene() => LoadScene("GameScene.unity");

    [MenuItem("Tools/SceneChange/DevScene &`")]
    private static void DevScene() => LoadScene("DevScene.unity");





}
#endif