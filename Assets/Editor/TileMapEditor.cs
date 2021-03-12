using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cargonia.Tile;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class TileMapEditor : EditorWindow
{
    private TileMap tileMap;
    
    private List<GameObject> assets;
    private List<GUIContent> assetIcons;
    private int assetIndex;

    [MenuItem("Editor Tools/TileMap Editor")]
    public static void ShowWindow()
    {
        GUIContent gUIContent = new GUIContent();
        gUIContent.text = "TileMap Editor";
        
        GetWindow(typeof(TileMapEditor)).titleContent = gUIContent;
        GetWindow(typeof(TileMapEditor)).Show();
    }
    
    private void OnSceneGUI(SceneView sceneView)
    {
        Vector2 tilePos = GetMousePosition();
    }

    private void OnEnable()
    {   
        tileMap = FindObjectOfType<TileMap>();
        
        SceneView.duringSceneGui += OnSceneGUI;

        LoadAssets();
    }

    private void LoadAssets()
    {
        assets.Clear();
        assetIcons.Clear();

        Object[] assetList = AssetDatabase.LoadAllAssetsAtPath("Resources");

        for (int i = 0; i < assets.Count; i++)
        {
            Texture2D texture = AssetPreview.GetAssetPreview(assetList[i]);
            
            assetIcons.Add(new GUIContent(assetList[i].name, texture));
            assets.Add((GameObject)assetList[i]);
        }
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private Vector2 GetMousePosition() 
    {
        // Convert mouse position to world position by finding point where y = 0.
        Vector3 mousePosition = Event.current.mousePosition;
        mousePosition.y = SceneView.currentDrawingSceneView.camera.pixelHeight - mousePosition.y;
        mousePosition = SceneView.currentDrawingSceneView.camera.ScreenToWorldPoint(mousePosition);
        mousePosition.y = -mousePosition.y;

        mousePosition.z = 0;
        
        
        Debug.Log(mousePosition);
        return mousePosition;
    }
}
