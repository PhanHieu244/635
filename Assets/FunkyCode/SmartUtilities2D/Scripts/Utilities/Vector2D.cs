/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Representation of 2D points
/// </summary>
public class Vector2D {
	public double x;
	public double y;

	public static Vector2D Zero() {
		return(new Vector2D(0, 0));
	}

	/// <summary>
	/// Representation of 2D points
	/// </summary>
	public Vector2D(double px, double py){
		x = px;
		y = py;
	}

	/// <summary>
	/// Representation of 2D points
	/// </summary>
	public Vector2D(Vector2D point)
	{
		x = point.x;
		y = point.y;
	}
		
	/// <summary>
	/// Representation of 2D points
	/// </summary>
	public Vector2D(Vector2 point)
	{
		x = point.x;
		y = point.y;
	}

	public Vector2D Copy()
	{
		return(new Vector2D(x, y));
	}
		
	/// <summary>
	/// Set x and y components of an existing 2D vector
	/// </summary>
	public void Set(double px, double py)
	{
		x = px;
		y = py;
	}

	/// <summary>
	/// Set x and y components of an existing 2D vector
	/// </summary>
	public void Set (Vector2D point)
	{
		x = point.x;
		y = point.y;
	}

	/// <summary>
	/// Push 2D vector coordinates by given rotation and distance 
	/// </summary>
	public void Push(double rot, double distance)
	{
		Inc(System.Math.Cos(rot) * distance, System.Math.Sin(rot) * distance);
	}

	public void Push(double rot, double distance, Vector2 scale)
	{
		Inc(System.Math.Cos(rot) * distance * scale.x, System.Math.Sin(rot) * distance * scale.y);
	}

	public Vector2D InverseTransformPoint(Transform transform) {
		return(new Vector2D(transform.InverseTransformPoint(ToVector2())));
	}

	/// <summary>
	/// Increase 2D vector coordinates by given x and y coordinates
	/// </summary>
	public void Inc (double px, double py)
	{
		x += px;
		y += py;
	}

	/// <summary>
	/// Decrease 2D vector coordinates by given x and y coordinates
	/// </summary>
	public void Dec (double px, double py)
	{
		x -= px;
		y -= py;
	}

	/// <summary>
	/// Increase 2D vector coordinates by given 2D vector
	/// </summary>
	public void Inc (Vector2D point)
	{
		x += point.x;
		y += point.y;
	}

	/// <summary>
	/// Decrease 2D vector coordinates by given 2D vector
	/// </summary>
	public void Dec (Vector2D point)
	{
		x -= point.x;
		y -= point.y;
	}

	/// <summary>
	/// Distance between given 2D vectors
	/// </summary>
	public static double Distance(Vector2D a, Vector2D b)
	{
		return(Mathd.Distance(a.x, a.y, b.x, b.y));
	}

	/// <summary>
	/// Angle between two given 2D coordinates
	/// </summary>
	public static double Atan2(Vector2D a, Vector2D b)
	{
		return(System.Math.Atan2 (a.y - b.y, a.x - b.x));
	}

	public static double Atan2(Vector2 a, Vector2 b)
	{
		return(System.Math.Atan2 (a.y - b.y, a.x - b.x));
	}

	public static Vector2D RotToVec(double rotation) {
		return(new Vector2D(System.Math.Cos(rotation), System.Math.Sin(rotation)));
	}

	public static double VecToRot(Vector2 vec) {
		return(System.Math.Atan2(vec.y, vec.x));
	}

	public static Vector2D operator +(Vector2D c1, Vector2D c2)
    {
        return new Vector2D(c1.x + c2.x, c1.y + c2.y);
    }

	public Vector2 ToVector2() {
		return(new Vector2((float)x, (float)y));
	}

	public Vector3 ToVector3(float z = 0) {
		return(new Vector3((float)x, (float)y, z));
	}

}
