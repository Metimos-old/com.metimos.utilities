using UnityEngine;

namespace Metimos
{
	[AddComponentMenu("Metimos/Debug/Grid Drawer")]
	public class GridDrawer : MonoBehaviour
	{
		public Transform follow;
		public float size = 10f;
		public float spacing = 5f;
		public float drawSize = 0.95f;
		
		private Grid _grid;
		
		private void OnEnable()
		{
			_grid = new(size);
		}

		private void OnValidate()
		{
			if (_grid == null) return;
			
			_grid.Size = size;
			_grid.spacing = spacing;
		}

		private void OnDrawGizmos()
		{
			if (follow == null || _grid == null)
				return;
			
			Vector3 position = follow.position;
			Vector2[] grids = _grid.GetNearbyGrids(position.x, position.z);
			
			for (var i = 0; i < grids.Length; i++)
			{
				Gizmos.color = i == 0 ? Color.cyan : Color.yellow;
				
				Bounds bounds = _grid.GetBounds(grids[i]);
				
				Gizmos.DrawWireCube(bounds.center, bounds.size * drawSize);
			}
		}
	}
}