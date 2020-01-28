﻿using System;
using System.CodeDom;
using LeoDeg.Math.Matrices;

namespace LeoDeg.Math.Vectors
{
	public class Vector3
	{
		#region Static Fields

		public static readonly Vector3 Up = new Vector3 (0, 1, 0);
		public static readonly Vector3 Down = new Vector3 (0, -1, 0);
		public static readonly Vector3 Left = new Vector3 (-1, 0, 0);
		public static readonly Vector3 Right = new Vector3 (1, 0, 0);
		public static readonly Vector3 Forward = new Vector3 (0, 0, 1);
		public static readonly Vector3 Backward = new Vector3 (0, 0, -1);

		public static readonly Vector3 Zero = new Vector3 (0, 0, 0);
		public static readonly Vector3 Identity = new Vector3 (1, 1, 1);

		#endregion

		#region Constructors

		public Vector3 (float x = 0, float y = 0, float z = 0)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public Vector3 (Vector2 vector)
		{
			if (vector == null)
				throw new ArgumentNullException ();

			x = vector.x;
			y = vector.y;
			z = 0;
		}

		public Vector3 (Vector3 vector)
		{
			if (vector == null)
				throw new ArgumentNullException ();

			x = vector.x;
			y = vector.y;
			z = vector.z;
		}

		public Vector3 (float[] vector3)
		{
			if (vector3.Length != 3)
				throw new InvalidOperationException ("Vector3::Error:: The array length not equal to 3.");

			x = vector3[0];
			y = vector3[1];
			z = vector3[2];
		}

		#endregion

		public float this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return x;
					case 1: return y;
					case 2: return z;
					default: throw new ArgumentOutOfRangeException ();
				}
			}
			set
			{
				switch (index)
				{
					case 0: x = value; break;
					case 1: y = value; break;
					case 2: z = value; break;
					default: throw new ArgumentOutOfRangeException ();
				}
			}
		}

		#region Properties

		public float x { get; set; }
		public float y { get; set; }
		public float z { get; set; }

		/// <summary>
		/// Return current vector in unit form.
		/// </summary>
		public Vector3 normalized => Normalize (this);

		/// <summary>
		/// Return magnitude of the current vector.
		/// </summary>
		public float magnitude => Magnitude (this);

		#endregion

		/// <summary>
		/// Make vector a unit vector.
		/// </summary>
		public static Vector3 Normalize (Vector3 vector)
		{
			return vector / Magnitude (vector);
		}

		#region Dot and Cross products

		/// <summary>
		/// Return dot product of two vectors.
		/// </summary>
		public static float Dot (Vector3 a, Vector3 b)
		{
			return (a.x * b.x) + (a.y * b.y) + (a.z * b.z);
		}

		/// <summary>
		/// Return dot product of the vector (v).
		/// </summary>
		public static float Dot (Vector3 v)
		{
			return (v.x * v.x) + (v.y * v.y) + (v.z * v.z);
		}

		/// <summary>
		/// Return dot product of three values.
		/// </summary>
		public static float Dot (float x, float y, float z)
		{
			return (x * x) + (y * y) + (z * z);
		}

		/// <summary>
		/// Return cross product between two vectors.
		/// </summary>
		public static Vector3 Cross (Vector3 from, Vector3 to)
		{
			return new Vector3
			(
				from.y * to.z - from.z * to.y,
				from.z * to.x - from.x * to.z,
				from.x * to.y - from.y * to.x

			);
		}

		#endregion

		#region Magnitude

		/// <summary>
		/// Return magnitude (speed) of two vectors
		/// </summary>
		public static float Magnitude (Vector3 a, Vector3 b)
		{
			return Convert.ToSingle (System.Math.Sqrt (Dot (a, b)));
		}

		/// <summary>
		/// Return magnitude (speed) of a vector.
		/// </summary>
		public static float Magnitude (Vector3 v)
		{
			return Convert.ToSingle (System.Math.Sqrt (Dot (v)));
		}

		#endregion

		#region Distance

		/// <summary>
		/// Return distance between 'from' and 'to'.
		/// </summary>
		public static float Distance (Vector3 from, Vector3 to)
		{
			float distX = to.x - from.x;
			float distY = to.y - from.y;
			float distZ = to.z - from.z;

			return Convert.ToSingle (System.Math.Sqrt (Dot (distX, distY, distZ)));
		}

		/// <summary>
		/// Return distance from current position and 'to'.
		/// </summary>
		public float Distance (Vector3 to)
		{
			float distX = to.x - this.x;
			float distY = to.y - this.y;
			float distZ = to.z - this.z;

			return Convert.ToSingle (System.Math.Sqrt (Dot (distX, distY, distZ)));
		}

		/// <summary>
		/// Return direction vector between two vectors.
		/// </summary>
		public static Vector3 Direction (Vector3 from, Vector3 to)
		{
			return to - from;
		}

		#endregion

		#region Angle

		/// <summary>
		/// Return angle between two vectors.
		/// </summary>
		public static float GetAngle (Vector3 from, Vector3 to)
		{
			float magnitude = Magnitude (from) * Magnitude (to);
			float dot = Dot (from, to);
			return dot / magnitude;
		}

		/// <summary>
		/// Check angle type: (0) is right angle, (1) is acute angle, (-1) is obtuse angle.
		/// </summary>
		/// <returns>
		/// (0) - is right angle
		/// (1) - is acute angle, 
		/// (-1) - is obtuse angle.
		/// </returns>
		public static int GetAngleType (Vector3 from, Vector3 to)
		{
			float angle = GetAngle (from, to);
			if (angle.Equals (0f))
				return 0;
			if (angle < 0)
				return -1;
			return 1;
		}

		/// <summary>
		/// Check angle type: (0) is right angle, (1) is acute angle, (-1) is obtuse angle.
		/// </summary>
		/// /// <returns>
		/// (0) - is right angle
		/// (1) - is acute angle, 
		/// (-1) - is obtuse angle.
		/// </returns>
		public static int GetAngleType (float angle)
		{
			if (angle.Equals (0f))
				return 0;
			if (angle < 0)
				return -1;
			return 1;
		}

		#endregion

		#region Projection

		/// <summary>
		/// Make projection of vector a onto vector b.
		/// <para>((a * b) / b^2) * b</para>
		/// </summary>
		public static Vector3 Project (Vector3 a, Vector3 b)
		{
			return b * (Dot (a, b) / Dot (b, b));
		}

		/// <summary>
		/// Make perpendicular vector from vector a to vector b.
		/// <para>a - (((a * b) / b^2 ) * b)</para>
		/// </summary>
		public static Vector3 Reject (Vector3 a, Vector3 b)
		{
			return a - (b * (Dot (a, b) / Dot (b, b)));
		}

		#endregion

		#region Equals

		public override bool Equals (object obj)
		{
			if (ReferenceEquals (null, obj)) return false;
			if (ReferenceEquals (this, obj)) return true;
			if (obj.GetType () != this.GetType ()) return false;

			return obj is Vector3 && this.Equals ((Vector3)obj);
		}

		/// <summary>
		/// Compares the passed vector to this one for equality.
		/// </summary>
		public bool Equals (Vector3 other)
		{
			if (ReferenceEquals (null, other)) return false;
			if (ReferenceEquals (this, other)) return true;

			return x.Equals (other.x) && y.Equals (other.y) && z.Equals (other.z);
		}

		#endregion

		#region Operators Overloading

		public static Vector3 operator + (Vector3 a, Vector3 b)
		{
			return new Vector3 (a.x + b.x, a.y + b.y, a.z + b.z);
		}

		public static Vector3 operator - (Vector3 a, Vector3 b)
		{
			return new Vector3 (a.x - b.x, a.y - b.y, a.z - b.z);
		}

		public static Vector3 operator - (Vector3 a)
		{
			return new Vector3 (-a.x, -a.y, -a.z);
		}

		public static Vector3 operator * (Vector3 a, float scalar)
		{
			return new Vector3 (a.x * scalar, a.y * scalar, a.z * scalar);
		}

		public static Vector3 operator * (float scalar, Vector3 a)
		{
			return new Vector3 (a.x * scalar, a.y * scalar, a.z * scalar);
		}

		public static Vector3 operator * (Matrice3 m, Vector3 v)
		{
			return new Vector3
			(
				m[0, 0] * v.x + m[0, 1] * v.y + m[0, 2] * v.z,
				m[1, 0] * v.x + m[1, 1] * v.y + m[1, 2] * v.z,
				m[2, 0] * v.x + m[2, 1] * v.y + m[2, 2] * v.z
			);
		}

		public static Vector3 operator / (Vector3 a, float scalar)
		{
			scalar = 1.0f / scalar;
			return new Vector3 (a.x * scalar, a.y * scalar, a.z * scalar);
		}

		public static bool operator == (Vector3 a, Vector3 b)
		{
			return a.Equals (b);
		}

		public static bool operator != (Vector3 a, Vector3 b)
		{
			return !a.Equals (b);
		}

		#endregion
	}
}
