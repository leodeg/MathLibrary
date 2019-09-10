﻿using LeoDeg.MathLib.Vectors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeoDeg.MathLib.Matrices
{
	public struct Matrice2 : IEnumerable
	{
		private float[,] m_Matrix;

		#region Constructors

		public Matrice2 (float[,] matrix)
		{
			m_Matrix = new float[2, 2];
			m_Matrix = matrix;
		}

		public Matrice2 (float n00, float n01, float n10, float n11)
		{
			m_Matrix = new float[2, 2];
			m_Matrix[0, 0] = n00;
			m_Matrix[0, 1] = n01;
			m_Matrix[1, 0] = n10;
			m_Matrix[1, 1] = n11;
		}

		public Matrice2 (Vector2 a, Vector2 b)
		{
			m_Matrix = new float[2, 2];
			m_Matrix[0, 0] = a.x;
			m_Matrix[0, 1] = a.y;
			m_Matrix[1, 0] = b.x;
			m_Matrix[1, 1] = b.y;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Allow to use array syntax.
		/// </summary>
		public float this[int a, int b]
		{
			get { return m_Matrix[a, b]; }
			set { m_Matrix[a, b] = value; }
		}

		/// <summary>
		/// Return zero matrix (0,0,0,0).
		/// </summary>
		public Matrice2 zero => new Matrice2 (0f, 0f, 0f, 0f);

		/// <summary>
		/// Return true if the current matrix is diagonal, otherwise return false.
		/// </summary>
		public bool isDiagonal => Diagonal (this);

		/// <summary>
		/// Transpose current matrix.
		/// </summary>
		public Matrice2 transposed => Transpose (this);

		/// <summary>
		/// Return true if the current matrix is symmetric, otherwise return false.
		/// </summary>
		public bool isSymmetric => Symmetric (this);

		/// <summary>
		/// Return true if the current matrix is antisymmetric, otherwise return false.
		/// </summary>
		public bool isAntiSymmetric => AntiSymmetric (this);

		/// <summary>
		/// Return determinant of the current matrix.
		/// </summary>
		public float determinant => Determinant (this);

		#endregion

		#region Helper Methods

		/// <summary>
		/// Transpose matrix.
		/// </summary>
		public static Matrice2 Transpose (Matrice2 matrix)
		{
			return new Matrice2 (matrix[0, 0], matrix[1, 0],
								 matrix[0, 1], matrix[1, 1]);
		}

		/// <summary>
		/// Return true if the matrix is diagonal, otherwise return false.
		/// </summary>
		public static bool Diagonal (Matrice2 matrix)
		{
			if (matrix[0, 1].Equals (0f) && matrix[1, 0].Equals (0f))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Return true if the matrix is symmetric, otherwise return false.
		/// </summary>
		public static bool Symmetric (Matrice2 matrix)
		{
			if (matrix[0, 1].Equals (matrix[1, 0]))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Return true if the matrix is antisymmetric, otherwise return false.
		/// </summary>
		public static bool AntiSymmetric (Matrice2 matrix)
		{
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Return determinant of the matrix.
		/// </summary>
		public static float Determinant (Matrice2 matrix)
		{
			return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
		}

		#endregion

		#region Math Operations

		/// <summary>
		/// Add two matrix together.
		/// </summary>
		public static Matrice2 Add (Matrice2 a, Matrice2 b) => a + b;

		/// <summary>
		/// Subtract second matrix from first matrix.
		/// </summary>
		public static Matrice2 Sub (Matrice2 a, Matrice2 b) => a - b;

		/// <summary>
		/// Multiply first matrix by second matrix.
		/// </summary>
		public static Matrice2 Mult (Matrice2 a, Matrice2 b) => a * b;

		/// <summary>
		/// Multiply matrix by vector.
		/// </summary>
		public static Matrice2 Mult (Matrice2 a, Vector2 b) => a * b;

		/// <summary>
		/// Multiply matrix by scalar value.
		/// </summary>
		public static Matrice2 Mult (Matrice2 a, float scalar) => a * scalar;

		/// <summary>
		/// Divide first matrix by second matrix.
		/// </summary>
		public static Matrice2 Div (Matrice2 a, Matrice2 b)
		{
			throw new NotImplementedException ();
		}

		#endregion

		#region Override Methods

		/// <summary>
		/// Compares the passed matrix to this one for equality.
		/// </summary>
		public override bool Equals (object obj)
		{
			if (ReferenceEquals (null, obj)) return false;
			if (ReferenceEquals (this, obj)) return true;
			if (obj.GetType () != this.GetType ()) return false;

			return obj is Matrice2 && this.Equals ((Matrice2)obj);
		}

		/// <summary>
		/// Compares the passed matrix to this one for equality.
		/// </summary>
		public bool Equals (Matrice2 other)
		{
			return m_Matrix[0, 0].Equals (other[0, 0])
				&& m_Matrix[0, 1].Equals (other[0, 1])
				&& m_Matrix[1, 0].Equals (other[1, 0])
				&& m_Matrix[1, 1].Equals (other[1, 1]);
		}

		public override int GetHashCode ()
		{
			return base.GetHashCode ();
		}

		public IEnumerator GetEnumerator () => m_Matrix.GetEnumerator ();

		#endregion

		#region Operators Overloading

		public static Matrice2 operator + (Matrice2 a, Matrice2 b)
		{
			return new Matrice2
			(
				a[0, 0] + b[0, 0],
				a[0, 1] + b[0, 1],
				a[1, 0] + b[1, 0],
				a[1, 1] + b[1, 1]
			);
		}

		public static Matrice2 operator - (Matrice2 a, Matrice2 b)
		{
			return new Matrice2
			(
				a[0, 0] - b[0, 0],
				a[0, 1] - b[0, 1],
				a[1, 0] - b[1, 0],
				a[1, 1] - b[1, 1]
			);
		}

		public static Matrice2 operator * (Matrice2 a, Matrice2 b)
		{
			return new Matrice2
			(
				a[0, 0] * b[0, 0] + a[0, 1] * b[1, 0],
				a[1, 0] * b[0, 0] + a[1, 1] * b[1, 0],
				a[1, 0] * b[0, 1] + a[0, 1] * b[1, 1],
				a[1, 0] * b[0, 1] + a[1, 1] * b[1, 1]
			);
		}

		public static Matrice2 operator * (Matrice2 a, Vector2 b)
		{
			return new Matrice2
			(
				a[0, 0] * b.x,
				a[1, 0] * b.y,
				a[1, 0] * b.x,
				a[1, 0] * b.y
			);
		}

		public static Matrice2 operator * (Matrice2 a, float scalar)
		{
			return new Matrice2
			(
				a[0, 0] * scalar,
				a[0, 1] * scalar,
				a[1, 0] * scalar,
				a[1, 1] * scalar
			);
		}

		public static Matrice2 operator * (float scalar, Matrice2 a)
		{
			return new Matrice2
			(
				a[0, 0] * scalar,
				a[0, 1] * scalar,
				a[1, 0] * scalar,
				a[1, 1] * scalar
			);
		}

		#endregion
	}
}
