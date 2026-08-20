using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefense.Core;
using System;

namespace TowerDefense.Maps
{
	/*
	Cria um mapa simples em 3D com lanes randomizadas
	Os dados gerados são mantidos separados de propósito
	*/
	public class MapGenerator : MonoBehaviour
	{
		[Header("Map Dimensions")]
		[SerializeField] private int seed = 2026;
		[SerializeField] private bool randomizeSeedOnPlay;
		[SerializeField] private int pathCount = 7;
		[SerializeField] private int waypointCount = 31;
		[SerializeField] private float mapLength = 200f;
		[SerializeField] private float pathSpacing = 20f;
		[SerializeField] private float pathVariation = 16f;

		private readonly List<EnemyPath> generatePaths = new List<EnemyPath>();
		private Transform generatedContent;

		public IReadOnlyList<EnemyPath> Paths => generatedPaths;
		public BaseCore BaseCore {get; private set;} 
		public float MapLength => mapLength;

		private void Awake()
		{
			GenerateMap();
		}

		public void GenerateMap()
		{
			ClearGeneratedContent();
			generatedPaths.Clear();

			int mapSeed = randomizeSeedOnPlay ? Environment.TickCount : seed;
			System.Random random = new System.Random(mapSeed);
			generatedContent = new GameObject("GeneratedMap").transform;
			generatedContent.SetParent(transform);

			CreateMaterialPallete(
				out Material floorMaterial, 
				out Material pathMaterial, 
				out Material baseMaterial);
			CreateFloor(floorMaterial);

			int safePathCount = Mathf.Max(2, pathCount);
			int safeWaypointCount = Mathf.Max(2, waypointCount);

			for (int pathIndex = 0; pathIndex < safePathCount; pathIndex++)
			{
				EnemyPath path = CreatePath(
					pathIndex,
					safePathCount,
					safeWaypointCount,
					random,
					pathMaterial );
				generatePaths.Add(path);
			}

			CreateBase(baseMaterial);
			ConfigureMainCamera();

			Debug.Log($"Generated map with {generatedPaths.Count} lanes using seed {mapSeed}");
		}

		public EnemyPath GetRandomPath()
		{
			if (generatedPaths.Count == 0)
			{
				return null;
			}

			return generatedPaths[UnityEngine.Random.Range(
				0, 
				generatedPaths.Count)];
		}

		public Vector3 ClampToPlayableArea(
			Vector3 position, float margin = 2f)
		{
			float halfLength = mapLength * 0.5f - margin;
			float halfWidth = Mathf.Max(6f, pathCount * pathSpacing * 0.5f + 3f) - margin;

			return new Vector3(
				Mathf.Clamp(position.x, -halfLength, halfLength),
				position.y,
				Mathf.Clamp(position.z, -halfWidth, halfWidth)
			);
		}

		public bool IsPositionLane(Vector3 position, float clearance)
		{
			foreach (EnemyPath path in generatedPaths)
			{
				if (path.GetClosestDistance(position) <= clearance)
				{
					return true;
				}
			}
			return false;
		}

		private EnemyPath CreatePath(int pathIndex, int totalPaths, int totalWaypoints, System.Random random, Material pathMaterial)
		{
			GameObject pathObject = new GameObject($"Lane_{pathIndex + 1:00}");
			pathObject.transform.SetParent(generatedContent);
			EnemyPath path = pathObject.AddComponent<EnemyPath>();

			List<Transform> waypoints = new List<Transform>();
			float baseZ;
			float curveAmplitude;
			float curveFrequency;
			float curvePhase;
			float smoothNoise;
			float smoothNoiseTarget;
			float startX;
			float segmentLength;
		}

		private void CreateFloor(Material floorMaterial)
		{

		}

		private void CreatePathVisual(Vector3 start, Vector3 end, Material pathMaterial, Transform parent)
		{

		}

		private void CreateSpawnMarker(Vector3 position, int pathIndex, Transform parent)
		{

		}

		private void CreateBase(Material baseMaterial)
		{

		}

		private void ConfigureMainCamera()
		{

		}

		private void CreateMaterialPallete(out Material floor, out Material path, out Material baseMaterial)
		{

		}

		private Material CreateMaterial(Color color)
		{

		}

		private void ClearGeneratedContent()
		{

		}


	}

}
