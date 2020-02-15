using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


class Node{
    public Node(Frame frame,Node parent,Rect rect,string edgeName)
    {
        this.rect = rect;
        this.frame = frame;
        this.parent = parent;
        this.edgeName = edgeName;
    }
    public bool active;
    public Frame frame;
    public Node parent;
    public Rect rect;
    public string edgeName;
}
class Edge
{
    public Edge(Node from,Node to,string edgeName) {
        this.from = from;
        this.to = to;
        this.edgeName = edgeName;
    }
    public Node from, to;
    public string edgeName;
}

public class StoryGraph : EditorWindow
{

    private Vector2 mousePosition;
    Dictionary<Frame, Node> Nodes;
    Dictionary<int, Frame> FrameIndex;

    List<Node> Graph;
    List<Edge> BackEdges;

    Rect all = new Rect(-1000, -1000, 30000, 30000);


    Vector2 scrollPos;
    Vector2 scrollStartPos;


    [MenuItem("Story/StoryGraph")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        StoryGraph window = (StoryGraph)EditorWindow.GetWindow(typeof(StoryGraph));
        window.Show();
    }
    private void OnEnable()
    {

        Graph = new List<Node>();
        BackEdges = new List<Edge>();
        Nodes = new Dictionary<Frame, Node>();
        //FrameIndex = new Dictionary<int, Frame>();
        Frame first = GameObject.Find("GameManager").GetComponent<TimelineManager>().InitialFrame;
        DFS(first, new Rect(position.width / 2 - 80 + scrollPos.x, 50 + 50 + scrollPos.y, 160, 50), null);
    }
    void OnGUI()
    {
        Event e = Event.current;
        mousePosition = e.mousePosition;
        DragMouse(e);
        GUILayout.BeginArea(all);
        BeginWindows();

        DrawGraph();
        EndWindows();
        GUILayout.EndArea();
    }

    private void DrawGraph()
    {
        for(int i = 0; i < Graph.Count; i++)
        {
            //Graph[i].rect.position += mousePosition;
            Texture2D tex = new Texture2D(1,1);
            GUI.Window(i, Graph[i].rect, DrawNodeWindow, Graph[i].frame.GetType() + "");
            if (Graph[i].parent != null)
            {
                //Graph[i].parent.rect.position += mousePosition;
                DrawNodeLine(Graph[i].parent.rect.position + Graph[i].parent.rect.size / 2, Graph[i].rect.position + Graph[i].rect.size / 2,Graph[i].edgeName);
                //Graph[i].parent.rect.position -= mousePosition;
            }
            //Graph[i].rect.position -= mousePosition;
        }
        foreach (var edge in BackEdges)
        {
            Rect r = edge.from.rect;
            Vector2 f = r.position;
            f.x += r.width / 2;
            f.y += r.height - 2;

            Rect r2 = edge.to.rect;
            Vector2 f2 = r2.position;
            f2.x += r2.width / 2;
            f2.y += 2;
            DrawNodeCurve(f, f2,edge.edgeName);
        }
    }


    private void DragMouse(Event e)
    {
        if (e.keyCode == KeyCode.R)
        {
            OnEnable();
        }
        if (e.button == 2)
        {
            if (e.type == EventType.MouseDown)
            {
                scrollStartPos = e.mousePosition;
            }
            else if (e.type == EventType.MouseDrag)
            {
                HandlePanning(e);
            }
            else if (e.type == EventType.MouseUp)
            {

            }
        }
        Repaint();
    }
    void HandlePanning(Event e)
    {
        Vector2 diff = e.mousePosition - scrollStartPos;
        diff *= .6f;
        scrollStartPos = e.mousePosition;
        scrollPos += diff;

        for (int i = 0; i < Graph.Count; i++)
        {
            Graph[i].rect.x += diff.x;
            Graph[i].rect.y += diff.y;
        }
    }

    public static void DrawNodeCurve(Vector2 from, Vector2 to,string how="")
    {
        Vector3 startPos = from;
        Vector3 endPos = to;
        Vector3 startTan = startPos + Vector3.right * Mathf.Abs(to.y - from.y);
        Vector3 endTan = endPos + Vector3.right * Mathf.Abs(to.y-from.y);
        Color shadow = new Color(0, 0, 0, 1);
        Handles.DrawBezier(startPos, endPos, startTan, endTan, shadow, null, 4);
        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.magenta, null, 3);
        var mid = Handles.MakeBezierPoints(startPos, endPos, startTan, endTan, 7)[4];

        var x = new GUIContent(how);
        var y = new GUIStyle { fontSize = 17, alignment = TextAnchor.MiddleCenter };
        //y.normal.textColor = new Color(1, 1, 1, 0.8f);
        y.fontSize = 16;
        Handles.Label(mid, x, y);
    }
    private void Update()
    {

        if (TimelineManager.Instance != null)
        {
            foreach (var node in Graph)
            {
                node.active = false;
            }
            if (TimelineManager.Instance.currentFrame != null)
                Nodes[TimelineManager.Instance.currentFrame].active = true;
            Repaint();
          
        }
    }
    public static void DrawNodeLine(Vector2 from, Vector2 to,string how="")
    {
        Vector3 startPos = from;
        Vector3 endPos = to;
        Vector3 startTan = startPos + Vector3.down;
        Vector3 endTan = endPos + Vector3.up;
        Color shadow = new Color(0, 0, 0, 1);
        Handles.DrawBezier(startPos, endPos, startTan, endTan, shadow, null, 4);
        Handles.DrawBezier(startPos, endPos, startTan, endTan, new Color(0.1f, 0.8f, 0.4f), null, 3);
        var mid = Vector3.Lerp(startPos, endPos, 0.5f);

        var x = new GUIContent(how);
        var y = new GUIStyle { fontSize = 17,alignment = TextAnchor.MiddleCenter };
        //y.normal.textColor = new Color(1, 1, 1, 0.8f);
        y.fontSize = 16;
        Handles.Label(mid, x, y);
    }
    private float DFS(Frame frame,Rect rect,Node from,string how="")
    {
        if (frame == null) return -10;
        if (Nodes.ContainsKey(frame))
        {
            BackEdges.Add(new Edge(from, Nodes[frame],how));
            return -10;
        }
        Node node = new Node(frame, from, rect,how);
        Nodes.Add(frame, node);
        Graph.Add(node);
        Rect nextRect=rect;
        nextRect.y += 150;
        if (frame.GetType()==typeof(Dialogue))
        {
            node.rect.height += 50;
            float get = DFS(((Dialogue)frame).nextFrame, nextRect, Graph[Graph.Count-1]);
            if (get >-1)
            {
                node.rect.x = get;
            }
        }
        else if (frame.GetType() == typeof(Choice))
        {
            int n = 0;
            var arr = ((Choice)frame).frames;
            nextRect = rect;
            nextRect.y += 100+(50*arr.Length);
            node.rect.height += 50*arr.Length;
            float xPos = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                float get = DFS(arr[i], nextRect, node,((Choice)frame).choices[i]);
                if (get > -1)
                {
                    n++;
                    xPos += get;
                    nextRect.x = get;
                    nextRect.x += 200;
                }
            }
            if (n > 0)
            {
                node.rect.x = xPos / n;
            }
        }
        else if (frame.GetType() == typeof(Action))
        {
            node.rect.height += 50;
            float get = DFS(((Action)frame).nextFrame, nextRect, node);
            if (get >-1)
            {
                node.rect.x = get;
            }
        }
        else if (frame.GetType() == typeof(XBranch))
        {
            int n = 0;
            nextRect = rect;
            nextRect.y += 200;
            float xPos = 0;
            node.rect.height += 100;

            float get = DFS(((XBranch)frame).trueFrame, nextRect, node,"true");
            if (get >-1)
            {
                n++;
                xPos += get;
                nextRect.x = get;
                nextRect.x += 200;
            }

            get = DFS(((XBranch)frame).falseFrame, nextRect, node,"false");
            if (get>-1)
            {
                n++;
                xPos += get;
                nextRect.x = get;
                nextRect.x += 200;
            }


            if (n > 0)
            {
                node.rect.x = xPos / n;
            }
        }
        else if (frame.GetType() == typeof(MiniGame))
        {
            int n = 0;
            nextRect = rect;
            nextRect.y += 200;
            float xPos = 0;
            node.rect.height += 100;

            float get = DFS(((MiniGame)frame).trueFrame, nextRect, node,"win");
            if (get >-1)
            {
                n++;
                xPos += get;
                nextRect.x = get;
                nextRect.x += 200;
            }

            get = DFS(((MiniGame)frame).falseFrame, nextRect, node,"lose");
            if (get >-1)
            {
                n++;
                xPos += get;
                nextRect.x = get;
                nextRect.x += 200;
            }


            if (n > 0)
            {
                node.rect.x = xPos / n;
            }
        }
        return node.rect.x;
    }
    private void DrawNodeWindow(int id)
    {
        if (Graph[id].active)
            GUI.color = Color.cyan;
        EditorGUILayout.ObjectField(Graph[id].frame, typeof(Frame), false, GUILayout.Width(150));
        if (Graph[id].frame.GetType() == typeof(Dialogue))
        {
            Dialogue d = Graph[id].frame as Dialogue;
            EditorGUILayout.LabelField("Next Frame");
            d.nextFrame = EditorGUILayout.ObjectField(d.nextFrame, typeof(Frame), false, GUILayout.Width(150)) as Frame;
        }
        if (Graph[id].frame.GetType() == typeof(Action))
        {
            Action d = Graph[id].frame as Action;
            EditorGUILayout.LabelField("Next Frame");
            d.nextFrame = EditorGUILayout.ObjectField(d.nextFrame, typeof(Frame), false, GUILayout.Width(150)) as Frame;
        }
        if (Graph[id].frame.GetType() == typeof(MiniGame))
        {
            MiniGame d = Graph[id].frame as MiniGame;
            EditorGUILayout.LabelField("Win Frame");
            d.trueFrame = EditorGUILayout.ObjectField(d.trueFrame, typeof(Frame), false, GUILayout.Width(150)) as Frame;
            EditorGUILayout.LabelField("Lose Frame");
            d.falseFrame = EditorGUILayout.ObjectField(d.falseFrame, typeof(Frame), false, GUILayout.Width(150)) as Frame;
        }
        if (Graph[id].frame.GetType() == typeof(XBranch))
        {
            XBranch d = Graph[id].frame as XBranch;
            EditorGUILayout.LabelField("True Frame");
            d.trueFrame = EditorGUILayout.ObjectField(d.trueFrame, typeof(Frame), false, GUILayout.Width(150)) as Frame;
            EditorGUILayout.LabelField("False Frame");
            d.falseFrame = EditorGUILayout.ObjectField(d.falseFrame, typeof(Frame), false, GUILayout.Width(150)) as Frame;
        }
        if (Graph[id].frame.GetType() == typeof(Choice))
        {
            Choice d = Graph[id].frame as Choice;
            if (d.frames.Length > 0)
            {
                EditorGUILayout.LabelField("Choice 1 Frame");
                d.frames[0] = EditorGUILayout.ObjectField(d.frames[0], typeof(Frame), false, GUILayout.Width(150)) as Frame;
            }
            if (d.frames.Length > 1)
            {
                EditorGUILayout.LabelField("Choice 2 Frame");
                d.frames[1] = EditorGUILayout.ObjectField(d.frames[1], typeof(Frame), false, GUILayout.Width(150)) as Frame;
            }
            if (d.frames.Length > 2)
            {
                EditorGUILayout.LabelField("Choice 3 Frame");
                d.frames[2] = EditorGUILayout.ObjectField(d.frames[2], typeof(Frame), false, GUILayout.Width(150)) as Frame;
            }
        }
    }
}
