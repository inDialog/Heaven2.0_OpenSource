﻿using System.Collections.Generic;
using UnityEngine;

public class ControlSystem : MonoBehaviour
{
    public List<Vector3> _wayPoints = new List<Vector3>();
    public List<LineProperties> lineProperties = new List<LineProperties>();
    bool needPoints = true;
    public bool randomMode;
    public Vector3 center;
    public bool noWStart;
    double extrude;
   public float z=0.1f;
    public void ActivateObject(bool active)
    {
        //needPoints = active;
        noWStart = !noWStart;
    }
    public void Random(bool active)
    {
        //needPoints = active;
        randomMode = !randomMode;

    }
    void Update()
    {

        if (lineProperties.Count > 50)
        {
            lineProperties.Clear();
            lineProperties = new List<LineProperties>();
        }

        if (!noWStart)
        {
            _wayPoints.Clear();
            return;
        }

        if (_wayPoints.Count == 4)
        {
            needPoints = true;
        }
        if (_wayPoints.Count < 1 & needPoints)
        {
            if (randomMode)
            {
                _wayPoints = GetComponent<CreateWayPoints>().PointsInstance(1);
                //FindObjectOfType<SendToArduino>()._positionsToSend[0].Add("G0Z" + z);
            }
            else
                FindObjectOfType<GameClient>().start = true;

            //z += 0.09f;

            needPoints = false;
        }



    }

    public LineProperties SetLine
    {
        set
        {
            _ = new LineProperties();
            LineProperties temp = value;
            lineProperties.Add(temp);
            FindObjectOfType<GraphicElements>().InstanceObject(temp);
            FindObjectOfType<GraphicElements>().CreatePoints(temp);

            //FindObjectOfType<SendToArduino>()._positionsToSend[0].Add(Printer3DComands(temp));
            FindObjectOfType<SendToArduino>()._positionsToSend[0].Add(Vplotter(temp));
            //FindObjectOfType<SendToArduino>()._positionsToSend[1].Add(CreateComandsS(temp));

            //FindObjectOfType<SendToArduino>()._positionsToSend[1].Add(ComandWall(temp, 0.6f));

        }

    }

    public Vector3 GetTarget
    {
        get
        {
            if (_wayPoints.Count > 0)
            {
                Vector3 temp = _wayPoints[0];
                _wayPoints.RemoveAt(0);
                return temp;
            }
            else
            {
                return Vector3.zero;
            }
        }
    }

    public Vector3 GetOld
    {
        get
        {
            if (_wayPoints.Count > 0)
            {
                Vector3 temp = _wayPoints[0];
                return temp;
            }
            else
            {
                return Vector3.zero;
            }
        }
    }

    //string Printer3DComands(LineProperties inLine)
    //{
    //    Vector2 inVector = new Vector2(inLine.endPosition.y + 70, inLine.endPosition.x);


    //    int scale = 1;
    //    string lineType = inLine.type;
    //    string comand = "null";

    //    if (lineType == "Arc")
    //    {
    //        if (lineProperties.Count > 1)
    //        {
    //            Vector3 pastVector = lineProperties[lineProperties.IndexOf(inLine) - 1].endPosition;
    //            comand = "G01X" + (pastVector.x) + "Y" + (pastVector.y+70);
    //        }
    //        if (inLine.LR == 1)
    //            comand = "G03X" + inVector.x * scale + "Y" + inVector.y * scale + "R" + inLine.radious * scale;
    //        if (inLine.LR == -1)
    //            comand = "G02X" + inVector.x * scale + "Y" + inVector.y * scale + "R" + inLine.radious * scale;
    //    }
    //    if (lineType == "Line")
    //    {
    //        comand = "G01X" + inVector.x * scale + "Y" + inVector.y * scale;
    //    }

    //    comand = comand + " Z" + z;
    //    extrude += 0.1;
    //    comand += " E"+extrude;

    //    return comand;
    //}
    string Vplotter(LineProperties inLine)
    {
        Vector2 inVector = new Vector2(inLine.endPosition.y , inLine.endPosition.x - 70);


        int scale = 2;
        string lineType = inLine.type;
        string comand = "null";
        if (lineType == "Arc")
        {
            if (lineProperties.Count > 1)
            {
                Vector3 pastVector = lineProperties[lineProperties.IndexOf(inLine) - 1].endPosition;
                comand = "G01X" + (pastVector.x) * scale + "Y" + (pastVector.y- 70) * scale;
            }
            if (inLine.LR == 1)
                comand = "G03X" + inVector.x * scale + "Y" + inVector.y * scale + "R" + inLine.radious * scale;
            if (inLine.LR == -1)
                comand = "G02X" + inVector.x * scale + "Y" + inVector.y * scale + "R" + inLine.radious * scale;
        }
        if (lineType == "Line")
        {
            comand = "G01X" + inVector.x * scale + "Y" + inVector.y * scale;
        }
        return comand;
    }
    string CreateComands(LineProperties inLine)
    {
        Vector2 inVector = new Vector2(inLine.endPosition.y - 125, inLine.endPosition.x - 180 - 70);
        

        int scale = 1;
        string lineType = inLine.type;
        string comand = "null";
        if (lineType == "Arc")
        {
            if (lineProperties.Count > 1)
            {
                Vector3 pastVector = lineProperties[lineProperties.IndexOf(inLine) - 1].endPosition;
                comand = "G01X" + (pastVector.x - 125) + "Y" + (pastVector.y  -180 - 70);
            }
            if (inLine.LR == 1)
                comand = "G03X" + inVector.x * scale + "Y" + inVector.y * scale + "R" + inLine.radious * scale;
            if (inLine.LR == -1)
                comand = "G02X" + inVector.x * scale + "Y" + inVector.y * scale + "R" + inLine.radious * scale;
        }
        if (lineType == "Line")
        {
            comand = "G01X" + inVector.x * scale + "Y" + inVector.y * scale;
        }
        return comand;
    }

    string CreateComandsS(LineProperties inLine)
    {
        Vector2 inVector = new Vector2(inLine.endPosition.y, inLine.endPosition.x-70);

        int scale = 1;
        string lineType = inLine.type;
        string comand = "null";
        if (lineType == "Arc")
        {
            if (lineProperties.Count > 1)
            {
                Vector3 pastVector = lineProperties[lineProperties.IndexOf(inLine) - 1].endPosition;
                comand = "G01X" + pastVector.y * scale + "Y" + (pastVector.x-70) * scale;
            }
            if (inLine.LR == 1)
                comand = "G03X" + inVector.x * scale + "Y" + inVector.y * scale + "R" + inLine.radious * scale;
            if (inLine.LR == -1)
                comand = "G02X" + inVector.x * scale + "Y" + inVector.y * scale + "R" + inLine.radious * scale;
        }
        if (lineType == "Line")
        {
            comand = "G01X" + inVector.x * scale + "Y" + inVector.y * scale;
        }
        return comand;
    }

    //string ComandWall(LineProperties inLine, float scale)
    //{
    //    Vector3 inVector = inLine.endPosition;

    //    string lineType = inLine.type;
    //    string comand = "null";

    //    if (lineType == "Arc")
    //    {
    //        //if (lineProperties.Count > 1)
    //        //    comand = "G01X" + -lineProperties[lineProperties.IndexOf(inLine) - 1].endPosition.y * scale + "Y" + -lineProperties[lineProperties.IndexOf(inLine) - 1].endPosition.x * scale;

    //        if (inLine.LR == 1)
    //            comand = "G03X" + -inVector.y * scale + "Y" + -inVector.x * scale + "R" + inLine.radious * scale;
    //        if (inLine.LR == -1)
    //            comand = "G02X" + -inVector.y * scale + "Y" + -inVector.x * scale + "R" + inLine.radious * scale;
    //    }
    //    if (lineType == "Line")
    //    {
    //        comand = "G01X" + -inVector.y * scale + "Y" + -inVector.x * scale;
    //    }
    //    return comand;
    //}

    //string CreateComands(LineProperties inLine)
    //{
    //    Vector3 inVector = inLine.endPosition;
    //    if (lineProperties.Count > 1)
    //    {
    //        Vector3 pastVector = lineProperties[lineProperties.IndexOf(inLine) - 1].endPosition;
    //    }

    //    int scale = 1;
    //    string lineType = inLine.type;
    //    string comand = "null";
    //    if (lineType == "Arc")
    //    {
    //        if (lineProperties.Count > 1)
    //            comand = "G01X" + lineProperties[lineProperties.IndexOf(inLine) - 1].endPosition.y * scale + "Y" + lineProperties[lineProperties.IndexOf(inLine) - 1].endPosition.x * scale;
    //        if (inLine.LR == 1)
    //            comand = "G03X" + (inLine.endPosition.y * scale + "Y" + inLine.endPosition.x * scale + "R" + inLine.radious * scale;
    //        if (inLine.LR == -1)
    //            comand = "G02X" + inLine.endPosition.y * scale + "Y" + inLine.endPosition.x * scale + "R" + inLine.radious * scale;
    //    }
    //    if (lineType == "Line")
    //    {
    //        comand = "G01X" + inLine.endPosition.y * scale + "Y" + inLine.endPosition.x * scale;
    //    }
    //    return comand;
    //}
}

