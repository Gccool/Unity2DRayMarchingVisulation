using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Raymarch : MonoBehaviour
{
    float SafeDis;
    public float MinCircle;
    public Transform RaycastHelp;
    public int maxRunAmount;
    public 
    // Start is called before the first frame update
    void Start()
    {
        
        Collider2D Overlap = Physics2D.OverlapCircle(transform.position, 1000);
        
        RaycastHelp.rotation = Quaternion.LookRotation((Overlap.transform.position - transform.position).normalized);
        
        SafeDis = Physics2D.Raycast(transform.position, RaycastHelp.forward).distance;

        RaycastHelp.rotation = transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {
        SafeDis = Mathf.Infinity;
        Collider2D[] Overlap = Physics2D.OverlapCircleAll(transform.position, 1000);
        float newDis = 0.0f;
        foreach(Collider2D  Overlap2 in Overlap)
        {
            RaycastHelp.rotation = Quaternion.LookRotation((Overlap2.transform.position - transform.position).normalized);

            newDis = Physics2D.Raycast(transform.position, RaycastHelp.forward).distance;
            
            if(newDis < SafeDis)
            {
                SafeDis = newDis;
            }

        }
        

        RaycastHelp.rotation = transform.rotation;
        DrawCircle(transform.position, SafeDis, 100, Color.red);
        RaycastHelp.position = transform.position + transform.up * SafeDis;
        float dis = 0.0f;
        int am = 0;
        for(bool i = true; i;)
        {
            Collider2D[] Overlap1 = Physics2D.OverlapCircleAll(transform.position, 1000);
            if(Overlap1.Length != 0)
            {
                dis = Mathf.Infinity;
                foreach(Collider2D Overlap3 in Overlap1)
                {
                    RaycastHelp.rotation = Quaternion.LookRotation((Overlap3.transform.position - RaycastHelp.position).normalized);
                    float newdis = Physics2D.Raycast(RaycastHelp.position, RaycastHelp.forward).distance;
                    if(newdis < dis)
                    {
                        dis = newdis;
                    }
                }
                DrawCircle(RaycastHelp.position, dis, 100, Color.red);
                RaycastHelp.rotation = transform.rotation;
                RaycastHelp.position = RaycastHelp.position + RaycastHelp.up * dis;
                if(dis <= MinCircle)
                {
                    i = false;
                }
            }
            else
            { 
                i = false;
            }
            if(am >= maxRunAmount)
            {
                i = false;
            }
            am += 1;
        }
    }

    public static void DrawCircle(Vector3 position, float radius, int segments, Color color)
    {
        // If either radius or number of segments are less or equal to 0, skip drawing
        if (radius <= 0.0f || segments <= 0)
        {
            return;
        }
    
        // Single segment of the circle covers (360 / number of segments) degrees
        float angleStep = (360.0f / segments);
    
        // Result is multiplied by Mathf.Deg2Rad constant which transforms degrees to radians
        // which are required by Unity's Mathf class trigonometry methods
    
        angleStep *= Mathf.Deg2Rad;
    
        // lineStart and lineEnd variables are declared outside of the following for loop
        Vector3 lineStart = Vector3.zero;
        Vector3 lineEnd = Vector3.zero;
    
        for (int i = 0; i < segments; i++)
        {
            // Line start is defined as starting angle of the current segment (i)
            lineStart.x = Mathf.Cos(angleStep * i) ;
            lineStart.y = Mathf.Sin(angleStep * i);
    
            // Line end is defined by the angle of the next segment (i+1)
            lineEnd.x = Mathf.Cos(angleStep * (i + 1));
            lineEnd.y = Mathf.Sin(angleStep * (i + 1));
    
            // Results are multiplied so they match the desired radius
            lineStart *= radius;
            lineEnd *= radius;
    
            // Results are offset by the desired position/origin 
            lineStart += position;
            lineEnd += position;
    
            // Points are connected using DrawLine method and using the passed color
            Debug.DrawLine(lineStart, lineEnd, color, 0.01f);
        }
    }


    


    public void render()
    {
        RaycastHelp.position = transform.position + transform.up * SafeDis;
        float dis = 0.0f;
        int am = 0;
        for(bool i = true; i;)
        {
            Collider2D Overlap = Physics2D.OverlapCircle(transform.position, 1000);
            if(Overlap)
            {
                RaycastHelp.rotation = Quaternion.LookRotation((Overlap.transform.position - RaycastHelp.position).normalized);
                dis = Physics2D.Raycast(RaycastHelp.position, RaycastHelp.forward).distance;
                DrawCircle(RaycastHelp.position, dis, 100, Color.red);
                RaycastHelp.rotation = transform.rotation;
                RaycastHelp.position = RaycastHelp.position + RaycastHelp.up * dis;
                if(dis <= MinCircle)
                {
                    i = false;
                }
            }
            else
            { 
                i = false;
            }
            if(am >= maxRunAmount)
            {
                i = false;
            }
            am += 1;
        }
    }
}
