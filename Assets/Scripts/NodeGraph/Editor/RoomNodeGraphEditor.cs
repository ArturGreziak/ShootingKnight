//using System.Collections.Generic;
//using UnityEditor;
//using UnityEditor.Callbacks;
//using UnityEngine;
//public class RoomNodeGraphEditor : EditorWindow
//{
//    private GUIStyle roomNodeStyle;


//    // Node layout values
//    private const float nodeWidth = 160f;
//    private const float nodeHeight = 75f;
//    private const int nodePadding = 25;
//    private const int nodeBorder = 12;
//    private RoomNodeTypeListSO roomNodeTypeList;


//    private static RoomNodeGraphSO currentRoomNodeGraph;

//    [MenuItem("Room Node Graph Editor", menuItem = "Window/Dungeon Editor/Room Node Graph Editor")]
//    private static void OpenWindow()
//    {
//        GetWindow<RoomNodeGraphEditor>("Room Node Graph Editor");
//    }

//    private void OnEnable()
//    {
//        // Define node layout style
//        roomNodeStyle = new GUIStyle();
//        roomNodeStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
//        roomNodeStyle.normal.textColor = Color.white;
//        roomNodeStyle.padding = new RectOffset(nodePadding, nodePadding, nodePadding, nodePadding);
//        roomNodeStyle.border = new RectOffset(nodeBorder, nodeBorder, nodeBorder, nodeBorder);
//    }

//    [OnOpenAsset(0)]  // Need the namespace UnityEditor.Callbacks
//    public static bool OnDoubleClickAsset(int instanceID, int line)
//    {
//        RoomNodeGraphSO roomNodeGraph = EditorUtility.InstanceIDToObject(instanceID) as RoomNodeGraphSO;
//        if (roomNodeGraph != null)
//        {
//            OpenWindow();

//            currentRoomNodeGraph = roomNodeGraph;

//            return true;
//        }
//        return false;
//    }



//    private void OnGUI()
//    {
//        // If a scriptable object of type RoomNodeGraphSO has been selected then process
//        if (currentRoomNodeGraph != null)
//        {

//            ProcessEvents(Event.current);

//            DrawRoomNodes();

//        }

//        if (GUI.changed)
//            Repaint();
//    }

//    private void ProcessEvents(Event currentEvent)
//    {
//        ProcessRoomNodeGraphEvents(currentEvent);
//    }

//    private void ProcessRoomNodeGraphEvents(Event currentEvent)
//    {
//        switch (currentEvent.type)
//        {
//            // Process Mouse Down Events
//            case EventType.MouseDown:
//                ProcessMouseDownEvent(currentEvent);
//                break;

//            //// Process Mouse Up Events
//            //case EventType.MouseUp:
//            //    ProcessMouseUpEvent(currentEvent);
//            //    break;

//            //// Process Mouse Drag Event
//            //case EventType.MouseDrag:
//            //    ProcessMouseDragEvent(currentEvent);

//            //    break;

//            default:
//                break;
//        }
//    }

//    private void ProcessMouseDownEvent(Event currentEvent)
//    {
//        // Process right click mouse down on graph event (show context menu)
//        if (currentEvent.button == 1)
//        {
//            ShowContextMenu(currentEvent.mousePosition);
//        }
//        // Process left mouse down on graph event
//        //else if (currentEvent.button == 0)
//        //{
//        //    ClearLineDrag();
//        //    ClearAllSelectedRoomNodes();
//        //}
//    }

//    private void ShowContextMenu(Vector2 mousePosition)
//    {
//        GenericMenu menu = new GenericMenu();

//        menu.AddItem(new GUIContent("Create Room Node"), false, CreateRoomNode, mousePosition);
//        menu.AddSeparator("");

//        menu.ShowAsContext();
//    }

//    private void CreateRoomNode(object mousePositionObject)
//    {
//        CreateRoomNode(mousePositionObject, roomNodeTypeList.list.Find(x => x.isNone));
//    }

//    private void CreateRoomNode(object mousePositionObject, RoomNodeTypeSO roomNodeType)
//    {
//        Vector2 mousePosition = (Vector2)mousePositionObject;

//        // create room node scriptable object asset
//        RoomNodeSO roomNode = ScriptableObject.CreateInstance<RoomNodeSO>();

//        // add room node to current room node graph room node list
//        currentRoomNodeGraph.roomNodeList.Add(roomNode);

//        // set room node values
//         roomNode.Initialise(new Rect(mousePosition, new Vector2(nodeWidth, nodeHeight)), currentRoomNodeGraph, roomNodeType);

//        // add room node to room node graph scriptable object asset database
//        AssetDatabase.AddObjectToAsset(roomNode, currentRoomNodeGraph);

//        AssetDatabase.SaveAssets();

//        // Refresh graph node dictionary
//        // currentRoomNodeGraph.OnValidate();
//    }

//    private void DrawRoomNodes()
//    {
//        foreach(RoomNodeSO roomNode in currentRoomNodeGraph.roomNodeList)
//        {
//            roomNode.Draw(roomNodeStyle);
//        }
//        GUI.changed = true;
//    }

//    //private void DrawRoomNodes()
//    //{

//    //}
//}
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class RoomNodeGraphEditor : EditorWindow
{
    private GUIStyle roomNodeStyle;

    // Node layout values
    private const float nodeWidth = 160f;
    private const float nodeHeight = 75f;
    private const int nodePadding = 25;
    private const int nodeBorder = 12;
    private RoomNodeTypeListSO roomNodeTypeList;

    private static RoomNodeGraphSO currentRoomNodeGraph;

    [MenuItem("Room Node Graph Editor", menuItem = "Window/Dungeon Editor/Room Node Graph Editor")]
    private static void OpenWindow()
    {
        GetWindow<RoomNodeGraphEditor>("Room Node Graph Editor");
    }

    private void OnEnable()
    {
        // Load RoomNodeTypeListSO from Resources folder or any other method you use to load ScriptableObjects
        roomNodeTypeList = Resources.Load<RoomNodeTypeListSO>("RoomNodeTypeList");

        // Define node layout style
        roomNodeStyle = new GUIStyle();
        roomNodeStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
        roomNodeStyle.normal.textColor = Color.white;
        roomNodeStyle.padding = new RectOffset(nodePadding, nodePadding, nodePadding, nodePadding);
        roomNodeStyle.border = new RectOffset(nodeBorder, nodeBorder, nodeBorder, nodeBorder);
    }

    [OnOpenAsset(0)]  // Need the namespace UnityEditor.Callbacks
    public static bool OnDoubleClickAsset(int instanceID, int line)
    {
        RoomNodeGraphSO roomNodeGraph = EditorUtility.InstanceIDToObject(instanceID) as RoomNodeGraphSO;
        if (roomNodeGraph != null)
        {
            OpenWindow();

            currentRoomNodeGraph = roomNodeGraph;

            return true;
        }
        return false;
    }

    private void OnGUI()
    {
        // If a scriptable object of type RoomNodeGraphSO has been selected then process
        if (currentRoomNodeGraph != null)
        {
            ProcessEvents(Event.current);

            DrawRoomNodes();
        }

        if (GUI.changed)
            Repaint();
    }

    private void ProcessEvents(Event currentEvent)
    {
        ProcessRoomNodeGraphEvents(currentEvent);
    }

    private void ProcessRoomNodeGraphEvents(Event currentEvent)
    {
        switch (currentEvent.type)
        {
            // Process Mouse Down Events
            case EventType.MouseDown:
                ProcessMouseDownEvent(currentEvent);
                break;

            //// Process Mouse Up Events
            //case EventType.MouseUp:
            //    ProcessMouseUpEvent(currentEvent);
            //    break;

            //// Process Mouse Drag Event
            //case EventType.MouseDrag:
            //    ProcessMouseDragEvent(currentEvent);

            //    break;

            default:
                break;
        }
    }

    private void ProcessMouseDownEvent(Event currentEvent)
    {
        // Process right click mouse down on graph event (show context menu)
        if (currentEvent.button == 1)
        {
            ShowContextMenu(currentEvent.mousePosition);
        }
        // Process left mouse down on graph event
        //else if (currentEvent.button == 0)
        //{
        //    ClearLineDrag();
        //    ClearAllSelectedRoomNodes();
        //}
    }

    private void ShowContextMenu(Vector2 mousePosition)
    {
        GenericMenu menu = new GenericMenu();

        menu.AddItem(new GUIContent("Create Room Node"), false, CreateRoomNode, mousePosition);
        menu.AddSeparator("");

        menu.ShowAsContext();
    }

    private void CreateRoomNode(object mousePositionObject)
    {
        if (roomNodeTypeList == null)
        {
            //Debug.LogError("RoomNodeTypeList is not loaded.");
            return;
        }

        var defaultRoomNodeType = roomNodeTypeList.list.Find(x => x.isNone);
        if (defaultRoomNodeType == null)
        {
            Debug.LogError("Default room node type not found.");
            return;
        }

        CreateRoomNode(mousePositionObject, defaultRoomNodeType);
    }

    private void CreateRoomNode(object mousePositionObject, RoomNodeTypeSO roomNodeType)
    {
        Vector2 mousePosition = (Vector2)mousePositionObject;

        // create room node scriptable object asset
        RoomNodeSO roomNode = ScriptableObject.CreateInstance<RoomNodeSO>();

        // add room node to current room node graph room node list
        currentRoomNodeGraph.roomNodeList.Add(roomNode);

        // set room node values
        roomNode.Initialise(new Rect(mousePosition, new Vector2(nodeWidth, nodeHeight)), currentRoomNodeGraph, roomNodeType);

        // add room node to room node graph scriptable object asset database
        AssetDatabase.AddObjectToAsset(roomNode, currentRoomNodeGraph);

        AssetDatabase.SaveAssets();

        // Refresh graph node dictionary
        // currentRoomNodeGraph.OnValidate();
    }

    private void DrawRoomNodes()
    {
        foreach (RoomNodeSO roomNode in currentRoomNodeGraph.roomNodeList)
        {
            roomNode.Draw(roomNodeStyle);
        }
        GUI.changed = true;
    }

}
