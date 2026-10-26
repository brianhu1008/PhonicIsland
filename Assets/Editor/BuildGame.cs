// Builds a Windows .exe of the game.
//   Menu:      Phonics Island > Build Windows EXE
//   Command:   Unity.exe -batchmode -quit -projectPath <project> -executeMethod PhonicsIsland.BuildGame.BuildWindows
// Output: Build/PhonicsIsland/PhonicsIsland.exe
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace PhonicsIsland
{
    public static class BuildGame
    {
        [MenuItem("Phonics Island/Build Windows EXE")]
        public static void BuildWindows()
        {
            EnsureBaseMaterial();
            EnsurePrimitiveMeshes();
            PlayerSettings.productName = "Phonics Island";
            PlayerSettings.fullScreenMode = FullScreenMode.Windowed;
            PlayerSettings.defaultScreenWidth = 1280;
            PlayerSettings.defaultScreenHeight = 720;
            PlayerSettings.resizableWindow = true;
            PlayerSettings.runInBackground = true;

            var options = new BuildPlayerOptions
            {
                scenes = new[] { "Assets/Scenes/SampleScene.unity" },
                locationPathName = "Build/PhonicsIsland/PhonicsIsland.exe",
                target = BuildTarget.StandaloneWindows64,
                options = BuildOptions.None
            };

            BuildReport report = BuildPipeline.BuildPlayer(options);
            Debug.Log("[BuildGame] Result: " + report.summary.result + ", size: " + report.summary.totalSize + " bytes");
            if (report.summary.result == BuildResult.Succeeded)
            {
                System.IO.File.WriteAllText("Build/PhonicsIsland/CREDITS.txt",
                    "Phonics Island - credits\r\n\r\n" +
                    "3D capybara: \"Capybara\" by Poly by Google, CC BY 3.0\r\n" +
                    "  https://poly.pizza/m/66d-mKAgF17  https://creativecommons.org/licenses/by/3.0/\r\n\r\n" +
                    "Sound effects: Kenney (www.kenney.nl), CC0\r\n" +
                    "  Interface Sounds, Impact Sounds, Music Jingles\r\n");
            }
            if (Application.isBatchMode && report.summary.result != BuildResult.Succeeded)
                EditorApplication.Exit(1);
        }

        // Web version for GitHub Pages: builds into docs/ (BuildWeb.bat runs this).
        [MenuItem("Phonics Island/Build Web (docs)")]
        public static void BuildWeb()
        {
            EnsureBaseMaterial();
            EnsurePrimitiveMeshes();
            PlayerSettings.productName = "Phonics Island";
            PlayerSettings.defaultWebScreenWidth = 1280;
            PlayerSettings.defaultWebScreenHeight = 720;
            PlayerSettings.runInBackground = true;
            // GitHub Pages can't send the Content-Encoding header, so the loader unpacks gzip itself.
            PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Gzip;
            PlayerSettings.WebGL.decompressionFallback = true;
            PlayerSettings.WebGL.dataCaching = true;
            PlayerSettings.WebGL.template = "PROJECT:Fill";   // Assets/WebGLTemplates/Fill: fills the browser window

            if (System.IO.Directory.Exists("docs")) System.IO.Directory.Delete("docs", true);
            var options = new BuildPlayerOptions
            {
                scenes = new[] { "Assets/Scenes/SampleScene.unity" },
                locationPathName = "docs",
                target = BuildTarget.WebGL,
                options = BuildOptions.None
            };

            BuildReport report = BuildPipeline.BuildPlayer(options);
            Debug.Log("[BuildGame] Web result: " + report.summary.result + ", size: " + report.summary.totalSize + " bytes");
            if (report.summary.result == BuildResult.Succeeded)
                System.IO.File.WriteAllText("docs/.nojekyll", "");   // serve every file as-is
            if (Application.isBatchMode && report.summary.result != BuildResult.Succeeded)
                EditorApplication.Exit(1);
        }

        // The game builds every material from code, so nothing in the scene references URP Lit
        // and the build strips it (everything turns magenta). A material in Resources keeps it.
        const string BaseMatPath = "Assets/Resources/PhonicsBaseLit.mat";

        static void EnsureBaseMaterial()
        {
            if (AssetDatabase.LoadAssetAtPath<Material>(BaseMatPath) != null) return;
            if (!AssetDatabase.IsValidFolder("Assets/Resources")) AssetDatabase.CreateFolder("Assets", "Resources");
            var mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            AssetDatabase.CreateAsset(mat, BaseMatPath);
            AssetDatabase.SaveAssets();
        }

        // The game builds everything from primitive shapes. GameObject.CreatePrimitive also adds a
        // collider, which the web build can't create (physics is stripped), so the meshes are saved
        // as assets and the game uses them directly.
        static void EnsurePrimitiveMeshes()
        {
            const string folder = "Assets/Resources/PrimMeshes";
            if (!AssetDatabase.IsValidFolder(folder)) AssetDatabase.CreateFolder("Assets/Resources", "PrimMeshes");
            foreach (PrimitiveType t in new[] { PrimitiveType.Sphere, PrimitiveType.Capsule, PrimitiveType.Cylinder, PrimitiveType.Cube, PrimitiveType.Plane, PrimitiveType.Quad })
            {
                string path = folder + "/" + t + ".asset";
                if (AssetDatabase.LoadAssetAtPath<Mesh>(path) != null) continue;
                GameObject g = GameObject.CreatePrimitive(t);
                Mesh copy = Object.Instantiate(g.GetComponent<MeshFilter>().sharedMesh);
                Object.DestroyImmediate(g);
                AssetDatabase.CreateAsset(copy, path);
            }
            AssetDatabase.SaveAssets();
        }
    }
}
