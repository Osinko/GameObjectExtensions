using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;


public class GameObjectExtensions : MonoBehaviour
{
}

[System.Serializable]
public struct IntVector2
{
		public int x, z;
		public IntVector2 (int x, int z)
		{
				this.x = x;
				this.z = z;
		}
	
		public static IntVector2 operator + (IntVector2 a, IntVector2 b)
		{
				a.x += b.x;
				a.z += b.z;
				return a;
		}

		/// <summary>
		/// 範囲内であるか調べます。範囲内であればtrueを返す
		/// </summary>
		/// <param name="value">調べたい値</param>
		/// <param name="range">範囲を指定する値</param>
		public static bool Contains (IntVector2 value, IntVector2 range)
		{
				return value.x >= 0 && value.x < range.x && value.z >= 0 && value.z < range.z;
		}
	
		public  bool Contains (IntVector2 range)
		{
				return this.x >= 0 && this.x < range.x && this.z >= 0 && this.z < range.z;
		}
	
		/// <summary>
		/// 乱数を返す
		/// </summary>
		/// <returns>IntVector2</returns>
		/// <param name="max">乱数の最大値</param>
		public static IntVector2 RandomVector (IntVector2 max)
		{
				return new IntVector2 (UnityEngine.Random.Range (0, max.x), UnityEngine.Random.Range (0, max.z));
		}
}


/// <summary>
/// GameObject 型の拡張メソッドを管理するクラス
/// </summary>
public static class GameObjectExtension
{
	
		/// <summary>
		/// コンポーネントを取得します
		/// コンポーネントが存在しなければ追加してから取得します
		/// </summary>
		/// <typeparam name="T">取得するコンポーネントの型</typeparam>
		/// <param name="gameObject">GameObject型のインスタンス</param>
		/// <returns>コンポーネント</returns>
		public static T SafeGetComponent<T> (this GameObject gameObject) where T : Component
		{
				return 
			gameObject.GetComponent<T> () ?? 
						gameObject.AddComponent<T> ();
		}
	
		/// <summary>
		/// 指定されたゲームオブジェクトが null または非アクティブであるかどうかを示します
		/// </summary>
		public static bool IsNullOrInactive (this GameObject self)
		{
				return self == null || !self.activeInHierarchy || !self.activeSelf;
		}
	
		/// <summary>
		/// 指定されたコンポーネントがアタッチされているかどうかを返します
		/// </summary>
		public static bool HasComponent<T> (this GameObject self) where T : Component
		{
				return self.GetComponent<T> () != null;
		}
	
		/// <summary>
		/// 向きを変更します
		/// </summary>
		public static void LookAt (this GameObject self, GameObject target)
		{
				self.transform.LookAt (target.transform);
		}
	
		/// <summary>
		/// 向きを変更します
		/// </summary>
		public static void LookAt (this GameObject self, Transform target)
		{
				self.transform.LookAt (target);
		}
	
		/// <summary>
		/// 向きを変更します
		/// </summary>
		public static void LookAt (this GameObject self, Vector3 worldPosition)
		{
				self.transform.LookAt (worldPosition);
		}
	
		/// <summary>
		/// 向きを変更します
		/// </summary>
		public static void LookAt (this GameObject self, GameObject target, Vector3 worldUp)
		{
				self.transform.LookAt (target.transform, worldUp);
		}
	
		/// <summary>
		/// 向きを変更します
		/// </summary>
		public static void LookAt (this GameObject self, Transform target, Vector3 worldUp)
		{
				self.transform.LookAt (target, worldUp);
		}
	
		/// <summary>
		/// 向きを変更します
		/// </summary>
		public static void LookAt (this GameObject self, Vector3 worldPosition, Vector3 worldUp)
		{
				self.transform.LookAt (worldPosition, worldUp);
		}
	
		public static bool HasChild (this GameObject gameObject)
		{
				return 0 < gameObject.transform.childCount;
		}
}

public static partial class TransformExtensions
{
		public static bool HasChild (this Transform transform)
		{
				return 0 < transform.childCount;
		}
}


/// <summary>
/// IList 型の拡張メソッドを管理するクラス
/// </summary>
public static partial class IListExtensions
{
		/// <summary>
		/// ランダムにリスト内の要素を返します
		/// </summary>
		/// <typeparam name="T">リスト要素の型</typeparam>
		/// <param name="self">リスト</param>
		/// <returns>リスト内の要素</returns>
		public static T GetRandom<T> (this IList<T> self)
		{
				return self [UnityEngine.Random.Range (0, self.Count)];
		}
}


/// <summary>
/// string 型の拡張メソッドを管理するクラス
/// </summary>
public static partial class StringExtensions
{
		/// <summary>
		/// 文字列を反転します
		/// </summary>
		public static string Reverse (this string text)
		{
				char[] cArray = text.ToCharArray ();
				string reverse = "";
				for (int i = cArray.Length-1; i > -1; i--) {
						reverse += cArray [i];
				}
				return reverse;
		}
	
		/// <summary>
		/// 指定された文字列で区切られた部分文字列を格納する文字列配列を返します
		/// </summary>
		public static string[] Split (this string self, string separator)
		{
				return self.Split (new []{ separator }, StringSplitOptions.None);
		}
	
		/// <summary>
		/// 指定された文字列で区切られた部分文字列を格納する文字列配列を返します
		/// </summary>
		public static string[] Split (this string self, string separator, StringSplitOptions options)
		{
				return self.Split (new []{ separator }, options);
		}
	
		/// <summary>
		/// 指定された文字列の配列の要素で区切られた部分文字列を格納する文字列配列を返します
		/// </summary>
		public static string[] Split (this string self, params string[] separator)
		{
				return self.Split (separator, StringSplitOptions.None);
		}
}