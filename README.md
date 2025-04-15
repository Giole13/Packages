# Log
2023.02.03 / GioleFunc+Obj.FindChildObject() bug fix
2023.02.06 / 
  GioleFunc+Obj.GetComponentMust Overloading =>
  this GameObjcet => findchild's Component
2023.03.07 / Add vscode Setting Json, Up-to-date
2023.03.07 /
  UnityEditor 씬 이동 스크립트, CSV => JSON 파일 변환 스크립트 추가
  설정 JSON 파일 수정  
2024.06.03 / Extenstion.cs 자식을 찾는 재귀함수 추가

# Tip
VsCode 변수 색상 변경
  - 설정 > Color customization > Edit in setting.json
  - https://velog.io/@ch0jm/VS-code-%EB%B3%80%EC%88%98-%EC%83%89%EC%83%81-%EB%B3%80%EA%B2%BD%ED%95%98%EB%8A%94%EB%B2%95

VsCode 유니티 개발환경 Extensions
  - C#, C# XML Documentation Comments, Unity Code Snippets, Unity Tools

# Scripts
DataTransformer.cs
  - CSV to Json Editor 확장 툴
ReadOnlyFieldAttribute.cs
  - 인스펙터에 노출하지만 값 수정은 X 에트리뷰트
SceneChange.cs
  - 단축키로 씬을 이동할 수 있는 editor 확장 툴
SlicedFilledImage.cs
  - 둥근 모양의 슬라이더를 표현하고 싶을때 찌그러 지지 않게 막아주는 Image 컴포넌트
Extensions.cs
  - 유니티에서 사용하기 좋은 확장 툴

# Libraray
UniTask
- https://github.com/Cysharp/UniTask
- https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask 링크를 Unity PackageManager에서 바로 붙여 사용 가능

# @visual Studio Code Snippet
vs에서 tools > code Snippet Manager 클릭 > add > 해당 폴더 추가

## 스니펫 xml 설명
`<Shortcut>여기에 vs에서 사용할 숏컷(단축어) 넣기</Shortcut>`
$ 표시는 $$ 형태로 두번 써야 표시된다.
```xml
<Declarations>
  <Literal>
      <ID>MethodName</ID>
      <ToolTip>Input test method name</ToolTip>
      <Default>TestMethod</Default>
  </Literal>
</Declarations>
```
이런 형태로 사용한다면 위에서 `$MethodName$` 형태로 입력 부분을 설정해야 한다.

# 주석 단축키 변경 링크
https://velog.io/@dohui/Visual-Studio-%EC%A3%BC%EC%84%9D-%EB%8B%A8%EC%B6%95%ED%82%A4-%EB%B3%80%EA%B2%BD-ctrl
tools > option > enviroment > Keyboard > 