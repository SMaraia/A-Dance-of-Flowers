using UnityEngine;
using System.Collections;

// Stores position and dies over time.
struct Point
{
    public Vector2 pos;
    float initTime;

    public Point(Vector2 pos)
    {
        this.pos = pos;
        this.initTime = Time.time;
    }
    
    public bool CheckForDeath(){
        return Time.time - initTime > 1.5f;
    }
}

// Used to provide information about the intersection later
struct IntersectIndex
{
    public int x, y, z, w;
    public IntersectIndex(int x, int y, int z, int w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }
}

public class TrailManager : MonoBehaviour {

    Vector3 prevPos;
    Point[] myPoints = new Point[1000];
    int oldPoint = 0;
    int nextPoint = 1;

    RaycastHit2D[] hit;

    public GameObject myParticleSystem;
    
	// Use this for initialization
	void Start () {
        prevPos = this.transform.position;
        this.GetComponent<TrailRenderer>().time = 1.5f;
        myPoints[0] = new Point((Vector2)prevPos);
        hit = new RaycastHit2D[1];
	}

    // FixedUpdate is called at a fixed interval.
    void Update(){


        if (myPoints[oldPoint].CheckForDeath())
        {
            oldPoint++;
            if (oldPoint == nextPoint)
                oldPoint--;
            if (oldPoint == 1000)
            {
                oldPoint = 0;
            }

        }

        if ((this.transform.position - prevPos).sqrMagnitude >= .1f)
        {
            // Set this at the start so we don't have to check so much.
            prevPos = this.transform.position;

            myPoints[nextPoint] = new Point((Vector2)prevPos);
            nextPoint++;
            if (nextPoint == 1000)
            {
                nextPoint = 0;
            }
            else if (nextPoint == oldPoint)
            {
                Debug.Log("WARNING, ERROR.  TRAILMANAGER SCRIPT MYPOINTS ARRAY NOT BIG ENOUGH TO PREVENT OVERLAP");
            }
        }

        if (oldPoint <= nextPoint)
        {
            ArrayList possibleIntersects = intersectStraight();
            if (possibleIntersects.Count > 0)
            {

                for (int i = 0; i < possibleIntersects.Count; ++i)
                {

                    IntersectIndex myIntersect = (IntersectIndex)possibleIntersects[i];
                    float minX = myPoints[myIntersect.x].pos.x, minY = myPoints[myIntersect.x].pos.y,
                        maxX = myPoints[myIntersect.x].pos.x, maxY = myPoints[myIntersect.x].pos.y;

                    for (int x = myIntersect.x; x < myIntersect.z; ++x)
                    {

                        if (myPoints[x].pos.x < minX)
                            minX = myPoints[x].pos.x;
                        else if (myPoints[x].pos.x > maxX)
                            maxX = myPoints[x].pos.x;
                        if (myPoints[x].pos.y < minY)
                            minY = myPoints[x].pos.y;
                        else if (myPoints[x].pos.y > maxY)
                            maxY = myPoints[x].pos.y;
                    }

                    if (maxX - minX > 1.0f && maxY - minY > 1.0f)
                    {
                        
                        Debug.DrawLine(myPoints[myIntersect.x].pos, myPoints[myIntersect.y].pos, Color.red);
                        Instantiate(myParticleSystem, new Vector2(myPoints[myIntersect.x].pos.x, myPoints[myIntersect.x].pos.y), Quaternion.identity);

                        for (int j = 1; j < myIntersect.w - myIntersect.x; ++j)
                        {
                            int mask = 1 << 8;
                            Physics2D.LinecastNonAlloc(myPoints[myIntersect.x].pos, myPoints[myIntersect.x + j].pos, hit, mask);
                            Debug.DrawLine(myPoints[myIntersect.x].pos, myPoints[myIntersect.x + j].pos);
                            for (int k = 0; k < hit.Length; ++k)
                            {
                                if (!hit[k])
                                {
                                    break;
                                }
                                hit[k].collider.gameObject.GetComponent<CirclingHandler>().isCircled = true;
                            }
                            hit = new RaycastHit2D[1];
                        }
                    }
                }
            }
        }
        else
        {
            // So this is really important... but I will code it in the next patch.  It is what makes our memory work
            // all nice and cleanly.  Basically when newpoint is over 100 (it's at 1000 for right now)
            //  

            //int possibleIntersect = intersectCut();
            //if (possibleIntersect != -1)
            //{

            //}
        }
    }

    // Checks for intersections
    ArrayList intersectStraight()
    {
        ArrayList myIndices = new ArrayList();

        for (int i = oldPoint; i < nextPoint - 1; i++)
        {
            Debug.DrawLine((Vector3)myPoints[i].pos, (Vector3)myPoints[i + 1].pos);
            // Start at two so we don't deal connection issues.  We can increase it from 2 if
            // we want a bigger minimum girth in circle size, 5 might be good.  But the minimum
            // is i + 2
            for (int j = i + 5; j < nextPoint - 1; j++)
            {
                
                // Next check for line crossing.
                Vector4 segmentA = new Vector4(myPoints[i].pos.x, myPoints[i].pos.y,
                    myPoints[i + 1].pos.x, myPoints[i + 1].pos.y);
                Vector4 segmentB = new Vector4(myPoints[j].pos.x, myPoints[j].pos.y,
                    myPoints[j + 1].pos.x, myPoints[j + 1].pos.y);

                //First do bounding box check
                if (!boxCross(segmentA, segmentB))
                {
                    continue;
                }

                if (potentialLineCross(segmentA, segmentB) && potentialLineCross(segmentB, segmentA))
                {
                    myIndices.Add(new IntersectIndex(i, i + 1, j, j + 1));
                    
                }
            }
        }

        return myIndices;
    }

    bool boxCross(Vector4 segmentA, Vector4 segmentB)
    {

        return Mathf.Min(segmentA.x, segmentA.z) <= Mathf.Max(segmentB.x, segmentB.z) &&
                    Mathf.Max(segmentA.x, segmentA.z) >= Mathf.Min(segmentB.x, segmentB.z) &&
                    Mathf.Min(segmentA.y, segmentA.w) <= Mathf.Max(segmentB.y, segmentB.w) &&
                    Mathf.Max(segmentA.y,segmentA.w) >= Mathf.Min(segmentB.y, segmentB.w);
    }

    bool potentialLineCross(Vector4 segmentA, Vector4 segmentB)
    {
        return pointOnLine(new Vector2(segmentB.x, segmentB.y), segmentA) ||
            pointOnLine(new Vector2(segmentB.z, segmentB.w), segmentA) ||
            pointRightOfLine(new Vector2(segmentB.x, segmentB.y), segmentA) ^
            pointRightOfLine(new Vector2(segmentB.z, segmentB.w), segmentA);
    }

    bool pointOnLine(Vector2 point, Vector4 segment)
    {
        // Shift everything so the segment originates from 0,0
        Vector2 tempSeg = new Vector2(segment.z - segment.x, segment.w - segment.y);
        Vector2 tempPoint = new Vector2(point.x - segment.x, point.y - segment.y);

        // Epsilon = .001f
        return  Mathf.Abs(tempSeg.x * tempPoint.y - tempSeg.y * tempPoint.x) <= 0.001f;
    }

    bool pointRightOfLine(Vector2 point, Vector4 segment)
    {
        // Shift everything so the segment originates from 0,0
        Vector2 tempSeg = new Vector2(segment.z - segment.x, segment.w - segment.y);
        Vector2 tempPoint = new Vector2(point.x - segment.x, point.y - segment.y);

        return tempSeg.x * tempPoint.y - tempSeg.y * tempPoint.x < 0;
    }

    
}
