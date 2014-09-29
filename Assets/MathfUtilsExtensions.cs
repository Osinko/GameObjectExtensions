using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class MathfUtilsExtensions : MonoBehaviour
{
}

/// <summary>
/// Mathf クラスに関する汎用関数を管理するクラス
/// </summary>
public static class MathfUtils
{

		/// <summary>
		/// 三角関数の加法定理の公式
		/// </summary>
		/// <returns>加算され回転した位置</returns>
		/// <param name="alphaPosition">正規化されたベクトル</param>
		/// <param name="betaRadian">ラジアン角</param>
		public static Vector2 TrigonAdditionTheorem (Vector2 alphaPosNorm, float betaRadian)
		{
				return new  Vector2 (alphaPosNorm.x * Mathf.Cos (betaRadian) - alphaPosNorm.y * Mathf.Sin (betaRadian),
		                     alphaPosNorm.y * Mathf.Cos (betaRadian) + alphaPosNorm.x * Mathf.Sin (betaRadian));
		}

		/// <summary>
		/// 順列の文字列リストを出力する
		/// </summary>
		/// <param name="n">全要素数</param>
		/// <param name="r">選ぶ数</param>
		/// <param name="strList">収納するstring型のリスト</param>
		public static void PermutationNumber (int n, int r, List <string> strList)
		{
				var number = Enumerable.Range (1, n).Select (x => x).ToArray ();
				PermutationNest (number, r, 0, "", strList);
		}
	
		static void PermutationNest (int[] n, int r, int columns, string resume, List <string> strList)
		{
				if (columns < r) {
						columns++;
						for (int i = 0; i < n.Length; i++) {
								string resumeClone = resume + n [i].ToString () + ",";
								int[] numClone = n.Where (x => x != n [i]).ToArray ();
								PermutationNest (numClone, r, columns, resumeClone, strList);
						}
				} else {
						strList.Add (resume);
				}
		}
	
	
		/// <summary>
		/// 組み合わせの文字列リストを出力する
		/// </summary>
		/// <param name="n">全要素数</param>
		/// <param name="r">選ぶ数</param>
		/// <param name="strList">収納するstring型のリスト</param>
		public static void CombinationNumbers (int n, int r, List <string> strList)
		{
				int[] numbers = new int[r + 1];
				CombinationNest (n, r, 1, 1, numbers, strList);
		}
	
		static void CombinationNest (int n, int r, int nest, int columns, int[] numbers, List <string> strList)
		{
				for (int i = nest; i <= n-r+columns; i++) {
						numbers [columns] = i;
						if (columns != r) {
								CombinationNest (n, r, i + 1, columns + 1, numbers, strList);
						} else {
								string str = "";
								for (int j = 1; j < numbers.Length; j++) {
										str += numbers [j] + ",";
								}
								strList.Add (str);
						}
				}
		}
	
	
		/// <summary>
		/// 順列の数を求める
		/// </summary>
		/// <param name="n">全要素数</param>
		/// <param name="r">選ぶ数</param>
		public static float PermutationNum (int n, int r)
		{
				return  Factor (n) / Factor (n - r);
		}
	
	
		/// <summary>
		/// 組み合わせの数を求める
		/// </summary>
		/// <param name="n">全要素数</param>
		/// <param name="r">選ぶ数</param>
		public static int CombinationNum (int n, int r)
		{
				return  Factor (n) / (Factor (n - r) * Factor (r));
		}
	
	
		/// <summary>
		/// 互いに素か判定する（互いに素だった場合true）
		/// </summary>
		/// <returns><c>true</c> if is coprime the specified x y; otherwise, <c>false</c>.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public static bool IsCoprime (float x, float y)
		{
				while (x!=y) {
						if (x > y) {
								x -= y;
						} else {
								y -= x;
						}
				}
				return x == 1;
		}
	
	
		/// <summary>
		/// 階乗計算
		/// </summary>
		/// <param name="n">数</param>
		public static int Factor (int n)
		{
				int ans = 1;
				for (int i=2; i <= n; i++) {
						ans *= i;
				}
				return ans;
		}
	
	
		/// <summary>
		/// 二分検索（このアルゴリズムはよく出来たので他でも使うとよい）
		/// </summary>
		/// <returns>The search.</returns>
		/// <param name="n">N.</param>
		/// <param name="list">List.</param>
		public static int  TreeSearch (float n, float[] list)
		{
				float start = 0;
				float end = list.Length;
				int mid;
		
				while (true) {
						mid = (int)(end - start) / 2;
						if (mid < 1)
								break;
						mid += (int)start;
			
						if (n > list [mid]) {
								start = mid;
						} else {
								end = mid;
						}
						//print (start + "  " + mid + "  " + end);
				}
				return mid + (int)start;
		}
	
	
		/// <summary>
		///ユークリッドの互除法
		///最小公約数を返す
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public static float Gcd (float x, float y)
		{
				while (y!=0) {
						float z = x % y;
						x = y;
						y = z;
				}
				return x;
		}
	
		/// <summary>
		///ユークリッドの互除法
		///最小公約数を返す。足算引き算のみの計算（高速？）
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public static float Gcd2 (float x, float y)
		{
				while (x!=y) {
						if (x > y) {
								x -= y;
						} else {
								y -= x;
						}
				}
				return x;
		}
	
		/// <summary>
		///ユークリッドの互除法
		///分数として扱い各値を約分する
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public static void Gcd (ref float x, ref float y)
		{
				float xx = x;
				float yy = y;
				while (yy!=0) {
						float z = xx % yy;
						xx = yy;
						yy = z;
				}
				x /= xx;
				y /= xx;
		}
	
		/// <summary>
		/// 最大公約数を返す
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public static float Lcm (float x, float y)
		{
				return x * y / Gcd (x, y);
		}
	
		/// <summary>
		/// 配列の指定要素を入れ替えます
		/// </summary>
		/// <param name="array">Array.</param>
		/// <param name="i">The index.</param>
		/// <param name="j">J.</param>
		public static void Swap<T> (T[] array, int i, int j)
		{
				T temp = array [i];
				array [i] = array [j];
				array [j] = temp;
		}
	
		/// <summary>
		/// 値を入れ替えるジェネリック関数
		/// </summary>
		/// <param name="lhs">ref必要　左項</param>
		/// <param name="rhs">ref必要　右項</param>
		/// <typeparam name="T">ジェネリック</typeparam>
		public static void Swap<T> (ref T lhs, ref T rhs)
		{
				T temp;
				temp = lhs;
				lhs = rhs;
				rhs = temp;
		}


		/// <summary>
		/// ブレゼンハムのアルゴリズム（４象限対応）
		/// </summary>
		/// <returns>The line.</returns>
		/// <param name="x0">始点X0.</param>
		/// <param name="y0">始点Y0.</param>
		/// <param name="x1">終点x1</param>
		/// <param name="y1">終点y1</param>
		public static IEnumerable<Vector2>  BresenhamsLine (int x0, int y0, int x1, int y1)
		{
				//正=true,負＝false
				int a = ((y0 * y1) > 0) ? Mathf.Abs (Mathf.Abs (y0) - Mathf.Abs (y1)) : Mathf.Abs (y0) + Mathf.Abs (y1);
				bool incrementA = ((y0 - y1) > 0) ? incrementA = false : incrementA = true;
		
				int b = ((x0 * x1) > 0) ? Mathf.Abs (Mathf.Abs (x0) - Mathf.Abs (x1)) : Mathf.Abs (x0) + Mathf.Abs (x1);
				bool incrementB = ((x0 - x1) > 0) ? incrementB = false : incrementB = true;
		
				bool changeAB = false;
				if (a > b) {
						MathfUtils.Swap (ref a, ref b);
						MathfUtils.Swap (ref x0, ref y0);
						MathfUtils.Swap (ref incrementA, ref incrementB);
						changeAB = true;
				}
		
				int a2 = a << 2;
				int b2 = b << 2;
				int D = -b;
		
				for (int x = 0,y = 0; x <= b; x++) {
						if (D > 0) {
								y = y + 1;
								D -= b2;
						}
						D += a2;
						if (changeAB) {
								yield return new Vector2 (y0 + (incrementA ? y : -y), x0 + (incrementB ? x : -x));
						} else {
								yield return new Vector2 (x0 + (incrementB ? x : -x), y0 + (incrementA ? y : -y));
						}
				}
		}


		/// <summary>
		/// 乗算する事でポンドをニュートンへ
		/// </summary>
		public const float Lb2Newton = 1 / 0.2248f;
	
	
		/// <summary>
		/// 乗算する事でニュートンをポンドへ
		/// </summary>
		public const float Newton2Lb = 0.2248f / 1;
	
	
		/// <summary>
		/// 傾きを求める
		/// x=nもしくはy=nのような直線の場合は傾き０として返すので注意
		/// </summary>
		/// <returns>傾き</returns>
		/// <param name="vec">ベクター</param>
		public static float _Vector2Slope (Vector2 vec)
		{
				return vec.y / vec.x;
		}
	
	
		/// <summary>
		/// 傾きを求める
		/// 与えられた２点間の直線の正規化された傾きを計算する
		/// </summary>
		/// <returns>傾き</returns>
		/// <param name="vec1">ベクター１</param>
		/// <param name="vec2">ベクター２</param>
		public static Vector2 Vector2Slope (Vector2 vec1, Vector2 vec2)
		{
				return (vec1 - vec2).normalized;
		} 
	
	
		/// <summary>
		/// ふたつの傾きが平行か調べる
		/// 正規化を使うのでコストは高い。また扱う桁の幅が広い場合、精度に問題が多い・資料
		/// http://dendrocopos.jp/tips/3d.html
		/// </summary>
		/// <returns><c>true</c>平行である<c>false</c>平行でない</returns>
		/// <param name="vec1">ベクター１</param>
		/// <param name="vec2">ベクター２</param>
		public static bool _ParallelCheck (Vector2 vec1, Vector2 vec2)
		{
				float dot = Vector2.Dot (vec1.normalized, vec2.normalized);
				return (dot > 0.999999f || dot < -0.999999f);
		}
	
	
		/// <summary>
		/// ふたつの傾きが平行か調べる
		/// この方法が一番高速で精度も調整しやすい。資料
		/// http://dendrocopos.jp/tips/3d.html
		/// </summary>
		/// <returns><c>true</c>平行である<c>false</c>平行でない</returns>
		/// <param name="vec1">ベクター１</param>
		/// <param name="vec2">ベクター２</param>
		public static bool ParallelCheck (Vector2 vec1, Vector2 vec2)
		{
				float dot = vec1.x * vec2.y - vec1.y * vec2.x;
				return dot > -0.000001f && dot < 0.000001f;
		}
	
	
		/// <summary>
		/// 直線の傾きに対し垂直な直線の傾きを返す
		/// </summary>
		/// <returns>与えられた直線に垂直な直線の傾き</returns>
		/// <param name="slope">直線の傾き</param>
		public static float PerpSlope (float slope)
		{
				return -1 / slope;
		}
	
	
		/// <summary>
		/// 直線の傾きに対し垂直な直線の傾きを返す
		/// </summary>
		/// <returns>与えられた直線に垂直な直線の傾き</returns>
		/// <param name="vec">直線の傾き</param>
		public static Vector2 PerpSlope (Vector2 vec)
		{
				return new Vector2 (-vec.y, vec.x);
		}
	
	
		/// <summary>
		/// ふたつの傾きが垂直か調べる
		/// 内積を使った垂直チェックは各ベクトルの正規化が必要ない（長さを無視できる）
		/// 角度を調べる際、これは非常に高速で有効な手段となる
		/// </summary>
		/// <returns><c>true</c>垂直である<c>false</c>垂直でない</returns>
		/// <param name="vec1">ベクター１</param>
		/// <param name="vec2">ベクター２</param>
		public static bool PerpCheck (Vector2 vec1, Vector2 vec2)
		{
				float dot = vec1.x * vec2.x + vec1.y * vec2.y;
				return dot > -0.00000001f && dot < 0.00000001f;
				//floatという型を使う以上ゼロの近傍範囲でもって判定することになります
				//これは将来、ゲーム内で扱う桁数が増えてくると精度の問題と直結してきます
		}
	
	
		/// <summary>
		/// ふたつの直線から交点を求める
		/// ターゲットに向かう傾きを採用して無限線同士の交点を求める
		/// </summary>
		/// <returns>交点・直線同士が平行の場合[0,0]を返す</returns>
		/// <param name="origin1">原点１</param>
		/// <param name="target1">ターゲット１</param>
		/// <param name="origin2">原点２</param>
		/// <param name="target2">ターゲット２</param>
		public static Vector2 LineIntersect (Vector2 origin1, Vector2 target1, Vector2 origin2, Vector2 target2)
		{
				if (ParallelCheck (origin1 - target1, origin2 - target2)) {
						return Vector2.zero;
				}    //平行チェック
				Vector2 intersect = Vector2.zero;
		
				Vector2 slopeV1 = origin1 - target1;
				float slopeF1 = slopeV1.y / slopeV1.x;
		
				Vector2 slopeV2 = origin2 - target2;
				float slopeF2 = slopeV2.y / slopeV2.x;
		
				intersect.x = (slopeF1 * origin1.x - slopeF2 * origin2.x + origin2.y - origin1.y) / (slopeF1 - slopeF2);
				intersect.y = slopeF1 * (intersect.x - origin1.x) + origin1.y;
		
				return intersect;
		}
	
	
		/// <summary>
		/// ふたつのレイの交点を求める
		/// Ray2DのDirection（Vector2）はターゲットの座標では無い事に注意。プロパティset時に自動的に正規化され傾きとして扱われている
		/// </summary>
		/// <returns>交点・直線同士が平行の場合[0,0]を返す</returns>
		/// <param name="ray1">レイ１</param>
		/// <param name="ray2">レイ２</param>
		public static Vector2 LineIntersect (Ray2D ray1, Ray2D ray2)
		{
				if (ParallelCheck (ray1.direction, ray2.direction)) {
						return Vector2.zero;
				}    //平行チェック
				Vector2 intersect = Vector2.zero;
		
				float slopeF1 = ray1.direction.y / ray1.direction.x;
		
				float slopeF2 = ray2.direction.y / ray2.direction.x;
		
				intersect.x = (slopeF1 * ray1.origin.x - slopeF2 * ray2.origin.x + ray2.origin.y - ray1.origin.y) / (slopeF1 - slopeF2);
				intersect.y = slopeF1 * (intersect.x - ray1.origin.x) + ray1.origin.y;
		
				return intersect;
		}
	
	
		/// <summary>
		/// 最終加速度を求める
		/// </summary>
		/// <returns>The f.</returns>
		/// <param name="vi">初速度（ベクター）</param>
		/// <param name="a">加速度（ベクター）</param>
		/// <param name="t">時間</param>
		public static Vector2 VelocityF (Vector2 vi, Vector2 a, float t)
		{
				return vi + a * t;
		}
	
	
		/// <summary>
		/// 移動変位を求める
		/// </summary>
		/// <returns>移動変位</returns>
		/// <param name="vi">初速度</param>
		/// <param name="a">加速度</param>
		/// <param name="t">時間</param>
		public static float DeltaX_a (float vi, float a, float t)
		{
				return vi * t + 0.5f * a * t * t;
		}
	
	
		/// <summary>
		/// 移動変位を求める
		/// </summary>
		/// <returns>移動変位</returns>
		/// <param name="t">時間</param>
		/// <param name="vi">初速度</param>
		/// <param name="vf">最終速度</param>
		public static float DeltaX_b (float t, float vi, float vf)
		{
				return 0.5f * (vi + vf) * t;
		}
	
	
		/// <summary>
		/// 移動変位を求める
		/// </summary>
		/// <returns>移動変位（ベクター）</returns>
		/// <param name="vi">初速度（ベクター）</param>
		/// <param name="a">加速度（ベクター）</param>
		/// <param name="t">時間</param>
		public static Vector2 DeltaVector2 (Vector2 vi, Vector2 a, float t)
		{
				return vi * t + 0.5f * a * t * t;
		}
	
		/// <summary>
		/// 移動変位を求める
		/// </summary>
		/// <returns>移動変位（ベクター）</returns>
		/// <param name="t">時間</param>
		/// <param name="vi">初速度（ベクター）</param>
		/// <param name="vf">最終速度（ベクター）</param>
		public static Vector2 DeltaVector2 (float t, Vector2 vi, Vector2 vf)
		{
				return 0.5f * (vi + vf) * t;
		}
	
	
		/// <summary>
		/// ふたつの境界球の衝突判定
		/// </summary>
		/// <returns><c>true</c>衝突した<c>false</c>衝突しなかった</returns>
		/// <param name="position1">位置1</param>
		/// <param name="position2">位置2</param>
		/// <param name="radius1">半径1</param>
		/// <param name="radius2">半径2</param>
		public static bool ColBetweenSphere (Vector3 position1, Vector3 position2, float radius1 = 1.0f, float radius2 = 1.0f)
		{
				return  (position1 - position2).sqrMagnitude <= (radius1 + radius2) * (radius1 + radius2);
		}
	
	
		/// <summary>
		/// ふたつの境界円の衝突判定
		/// </summary>
		/// <returns><c>true</c>衝突した<c>false</c>衝突しなかった</returns>
		/// <param name="position1">位置1</param>
		/// <param name="position2">位置2</param>
		/// <param name="radius1">半径1</param>
		/// <param name="radius2">半径2</param>
		public static bool ColBetweenSphere (Vector2 position1, Vector2 position2, float radius1 = 1.0f, float radius2 = 1.0f)
		{
				return  (position1 - position2).sqrMagnitude <= (radius1 + radius2) * (radius1 + radius2);
		}
	
	
		//単位：ジュールを返す
		public static float CalcWork (float force, float friction, float deltaX)
		{
				return deltaX * (force - friction);
		}
	
	
		/// <summary>
		/// 合力の演算に必要なパラメータから加速度を返す
		/// </summary>
		/// <returns>The velocity.</returns>
		/// <param name="forceN">加える力（単位：N）</param>
		/// <param name="degree">加える力の角度（単位：°）</param>
		/// <param name="weightN">重さ（単位：N）</param>
		/// <param name="mueK">摩擦係数</param>
		/// <param name="gravity">重力（通常9.8）</param>
		public static Vector2 CalcVelocity (float forceN, float degree, float weightN, float mueK, float gravity=9.8f)
		{
				return CalcVelocity (Force (forceN, degree, weightN, mueK), CalcMass (weightN, gravity));
		}
	
	
		public static Vector2 CalcVelocity (float forceN, float degree, float weightN, Vector2 fk, float gravity=9.8f)
		{
				return CalcVelocity (Force (forceN, degree, weightN, fk), CalcMass (weightN, gravity));
		}
	
	
		/// <summary>
		/// 合力と質量から加速度を計算する
		/// </summary>
		/// <returns>加速度（ベクトル）</returns>
		/// <param name="sigmaF">合力（ベクトル）</param>
		/// <param name="mass">質量（単位：kg）</param>
		public static Vector2 CalcVelocity (Vector2 sigmaF, float mass)
		{
				return sigmaF / mass;
		}
	
	
		/// <summary>
		/// 重さを計算する
		/// </summary>
		/// <returns>重さ（単位：N）</returns>
		/// <param name="mass_Kg">質量（単位：kg）</param>
		/// <param name="gravity">重力（通常9.8）</param>
		public static float CalcWeight (float mass, float gravity=9.8f)
		{
				return mass * gravity;
		}
	
	
		/// <summary>
		/// 質量を計算する
		/// </summary>
		/// <returns>質量（単位：kg）</returns>
		/// <param name="weightN">重さスカラー（単位：N）</param>
		/// <param name="gravity">重力（通常9.8）</param>
		public static float CalcMass (float weightN, float gravity=9.8f)
		{
				return weightN / gravity;
		}
	
	
		/// <summary>
		/// １要素の加えられた力から得られる合力を求める
		/// </summary>
		/// <returns>合力（ベクトル）</returns>
		/// <param name="forceN">加える力（単位：N）</param>
		/// <param name="degree">加える力の角度（単位：°）</param>
		/// <param name="weightN">重さ（単位：N）</param>
		/// <param name="mueK">摩擦係数</param>
		public static Vector2 Force (float forceN, float degree, float weightN, float mueK)
		{
				Vector2 fp, fk;
				fp = Polar2DescartesForce (degree, forceN);
				fk = CalcFriction (mueK, weightN, degree + 180);
				return fp + fk;
		}
	
	
		/// <summary>
		/// １要素の加えられた力から得られる合力を求める
		/// </summary>
		/// <returns>合力（ベクトル）</returns>
		/// <param name="forceVector">加える力（ベクトル）</param>
		/// <param name="weightN">重さ（単位：N）</param>
		/// <param name="mueK">摩擦係数</param>
		public static Vector2 Force (Vector2 forceVector, float weightN, float mueK)
		{
				Vector2 fp, fk;
				fp = forceVector;
				Vector2 flipVecNorm = (forceVector * -1);
				fk = CalcFriction (mueK, weightN, flipVecNorm);
				return fp + fk;
		}
	
	
		/// <summary>
		/// 加えられた力から得られる合力を求める
		/// （垂直抗力まで内部で求める）
		/// </summary>
		/// <returns>合力（ベクトル）</returns>
		/// <param name="forceN">加える力（単位：N）</param>
		/// <param name="degree">加える力の角度（単位：°）</param>
		/// <param name="weightN">重さ（単位：N）</param>
		/// <param name="fk">摩擦力（ベクトル）</param>
		public static Vector2 Force (float forceN, float degree, float weightN, Vector2 fk)
		{
				Vector2 fp, n, nDush;
				fp = Polar2DescartesForce (degree, forceN);
				n = new Vector2 (0, weightN);
				nDush = new Vector2 (0, -1 * (n.y + fp.y));
				return fp + fk + n + nDush;
		}
	
	
		/// <summary>
		/// 合力を求める
		/// </summary>
		/// <param name="fp">力（ベクトル）</param>
		/// <param name="fk">摩擦力や垂直抗力（ベクトル）</param>
		public static Vector2 Force (Vector2 fp, Vector2 fk)
		{
				return fp + fk;
		}
	
	
		/// <summary>
		/// 摩擦力を計算
		/// </summary>
		/// <returns>The friction.</returns>
		/// <param name="mue">摩擦係数</param>
		/// <param name="weightN">重さ（単位：N）</param>
		/// <param name="degree">摩擦力の働く向き（単位：° 垂直抗力の場合-90.0°になる）</param>
		public static Vector2 CalcFriction (float mue, float weightN, float degree=-90)
		{
				Vector2 vec = Polar2DescartesForce (degree, weightN);
				return mue * vec;
		}
	
	
		/// <summary>
		/// 摩擦力を計算
		/// </summary>
		/// <returns>The friction.</returns>
		/// <param name="mue">摩擦係数</param>
		/// <param name="weightN">重さ（単位：N）</param>
		/// <param name="vec">摩擦力の働く向き（例：垂直抗力なら(0,-1)になる</param>
		public static Vector2 CalcFriction (float mue, float weightN, Vector2 vec)
		{
				return mue * weightN * vec.normalized;
		}
	
	
		/// <summary>
		/// 極座標表現の力をデカルトの力へ変換
		/// </summary>
		/// <returns>デカルトの力</returns>
		/// <param name="degree">角度（単位：°）</param>
		/// <param name="forceN">力（単位：N）.</param>
		public static Vector2 Polar2DescartesForce (float degree, float forceN)
		{
				return Polar2Descartes (degree, forceN);
		}
	
	
		/// <summary>
		/// 極座標からデカルト座標へ変換
		/// 0°で(1,0)になる
		/// </summary>
		/// <returns>デカルト座標</returns>
		/// <param name="dir">角度（単位：°）</param>
		/// <param name="mag">長さ</param>
		public static Vector2 Polar2Descartes (float dir, float mag=1)
		{
				return new Vector2 (Mathf.Cos (dir * Mathf.Deg2Rad) * mag, Mathf.Sin (dir * Mathf.Deg2Rad) * mag);
		}
	
		/// <summary>
		/// デカルト座標から極座標へ変換
		/// 0°で(1,0)になる
		/// </summary>
		/// <returns>極座標</returns>
		/// <param name="vec">デカルト座標</param>
		public static Polar Descartes2Polar (Vector2 vec)
		{
				Polar polar2 = new Polar (0, 0);
				polar2.Mag = Mathf.Sqrt (vec.x * vec.x + vec.y * vec.y);
				if (polar2.Mag == 0)
						return polar2;
				polar2.Dir = Mathf.Asin (vec.y / polar2.Mag) * Mathf.Rad2Deg;
				if (vec.x < 0) {
						polar2.Dir = 180 - polar2.Dir;
				} else {
						if (vec.x > 0 && vec.y < 0) {
								polar2.Dir += 360;
						}
				}
				return polar2;
		}
	
	
		/// <summary>
		/// ２のべき乗かどうかをチェックする
		/// </summary>
		/// <returns><c>true</c>２のべき乗である<c>false</c>２のべき乗ではない</returns>
		/// <param name="n">N.</param>
		public static bool PowCheck (int n)
		{
				return (0 == (n & (n - 1)));
		}
	
		/// <summary>
		/// ベジェ曲線における X 座標を返します
		/// </summary>
		/// <remarks>http://opentype.jp/fontguide_doc3.htm</remarks>
		/// <param name="x1">開始点の X 座標</param>
		/// <param name="x2">制御点 1 の X 座標</param>
		/// <param name="x3">制御点 2 の X 座標</param>
		/// <param name="x4">終点の X 座標</param>
		/// <param name="t">重み(0 から 1)</param>
		/// <returns>ベジェ曲線における X 座標</returns>
		public static float BezierCurveX (float x1, float x2, float x3, float x4, float t)
		{
				return Mathf.Pow (1 - t, 3) * x1 + 3 * Mathf.Pow (1 - t, 2) * t * x2 + 3 * (1 - t) * Mathf.Pow (t, 2) * x3 + Mathf.Pow (t, 3) * x4;
		}
	
		/// <summary>
		/// ベジェ曲線における Y 座標を返します
		/// </summary>
		/// <remarks>http://opentype.jp/fontguide_doc3.htm</remarks>
		/// <param name="y1">開始点の Y 座標</param>
		/// <param name="y2">制御点 1 の Y 座標</param>
		/// <param name="y3">制御点 2 の Y 座標</param>
		/// <param name="y4">終点の Y 座標</param>
		/// <param name="t">重み(0 から 1)</param>
		/// <returns>ベジェ曲線における Y 座標</returns>
		public static float BezierCurveY (float y1, float y2, float y3, float y4, float t)
		{
				return Mathf.Pow (1 - t, 3) * y1 + 3 * Mathf.Pow (1 - t, 2) * t * y2 + 3 * (1 - t) * Mathf.Pow (t, 2) * y3 + Mathf.Pow (t, 3) * y4;
		}
	
		/// <summary>
		/// ベジェ曲線における 2 次元座標を返します
		/// </summary>
		/// <param name="p1">開始点の座標</param>
		/// <param name="p2">制御点 1 の座標</param>
		/// <param name="p3">制御点 2 の座標</param>
		/// <param name="p4">終点の座標</param>
		/// <param name="t">重み(0 から 1)</param>
		/// <returns>B-スプライン曲線における 2 次元座標</returns>
		public static Vector2 BezierCurve (Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, float t)
		{
				return new Vector2 (
			BezierCurveX (p1.x, p2.x, p3.x, p4.x, t), 
			BezierCurveY (p1.y, p2.y, p3.y, p4.y, t));
		}
	
	
		/// <summary>
		/// B-スプライン曲線における X 座標を返します
		/// </summary>
		/// <remarks>http://opentype.jp/fontguide_doc2.htm</remarks>
		/// <param name="x1">開始点の X 座標</param>
		/// <param name="x2">制御点の X 座標</param>
		/// <param name="x3">終点の X 座標</param>
		/// <param name="t">重み(0 から 1)</param>
		/// <returns>B-スプライン曲線における X 座標</returns>
		public static float B_SplineCurveX (float x1, float x2, float x3, float t)
		{
				return (1 - t) * (1 - t) * x1 + 2 * t * (1 - t) * x2 + t * t * x3;
		}
	
		/// <summary>
		/// B-スプライン曲線における Y 座標を返します
		/// </summary>
		/// <remarks>http://opentype.jp/fontguide_doc2.htm</remarks>
		/// <param name="y1">開始点の Y 座標</param>
		/// <param name="y2">制御点の Y 座標</param>
		/// <param name="y3">終点の Y 座標</param>
		/// <param name="t">重み(0 から 1)</param>
		/// <returns>B-スプライン曲線における Y 座標</returns>
		public static float B_SplineCurveY (float y1, float y2, float y3, float t)
		{
				return (1 - t) * (1 - t) * y1 + 2 * t * (1 - t) * y2 + t * t * y3;
		}
	
		/// <summary>
		/// B-スプライン曲線における 2 次元座標を返します
		/// </summary>
		/// <param name="p1">開始点の座標</param>
		/// <param name="p2">制御点の座標</param>
		/// <param name="p3">終点の座標</param>
		/// <param name="t">重み(0 から 1)</param>
		/// <returns>B-スプライン曲線における 2 次元座標</returns>
		public static Vector2 B_SplineCurveY (Vector2 p1, Vector2 p2, Vector2 p3, float t)
		{
				return new Vector2 (
			B_SplineCurveX (p1.x, p2.x, p3.x, t), 
			B_SplineCurveY (p1.y, p2.y, p3.y, t));
		}
}

/// <summary>
/// 極座標を表す構造体
/// </summary>
public struct Polar
{
		float mag;        //長さ
		float dir;        //角度（単位：°）
	
		/// <summary>
		/// 極座標のパラメーターを与えて初期化
		/// </summary>
		/// <param name="mag">長さ</param>
		/// <param name="dir">角度（単位：°）</param>
		public Polar (float mag, float dir)
		{
				this.dir = dir;
				this.mag = mag;
		}
	
		/// <summary>
		/// 角度（単位：°）
		/// </summary>
		/// <value>The dir.</value>
		public float Dir {
				get { return this.dir; }
				set { this.dir = value; }
		}
	
		/// <summary>
		/// 長さ
		/// </summary>
		/// <value>The mag.</value>
		public float Mag {
				get { return this.mag; }
				set { this.mag = value; }
		}
	
		/// <summary>
		/// 極座標からデカルト座標へ変換
		/// </summary>
		/// <returns>極座標から変換されたデカルト座標</returns>
		public Vector2 ConvertDescartes {
				get{ return new Vector2 (Mathf.Cos (dir * Mathf.Deg2Rad) * mag, Mathf.Sin (dir * Mathf.Deg2Rad) * mag); }
		}
	
		public override string ToString ()
		{
				return string.Format (mag + " @ " + dir + "°");
		}
}
