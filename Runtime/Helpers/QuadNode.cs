using System;
using System.Linq;
using UnityEngine;

namespace Metimos
{
	public abstract class QuadNode<TNode> : IDisposable where TNode : QuadNode<TNode>
	{
		public event Action<TNode> OnNodeAdded;
		public event Action<TNode> OnNodeRemoved;
			
		public TNode root;
		public TNode parent;
		public TNode[] children;
		public int index;
		public float width;
		public float height;
		public int depth;
		
		protected float _x;
		protected float _y;

		public bool IsDisposed { get; private set; }
		public bool IsLeaf => children == null;
		public bool IsBranch => children != null;
		public bool IsRoot => parent == null;

		public Vector2 Position
		{
			get => new(_x, _y);
			set
			{
				Vector2 delta = new(value.x - _x, value.y - _y);
				
				_x = value.x;
				_y = value.y;

				if (!IsBranch) return;
				foreach (TNode child in children)
					if (child != null)
						child.Position += delta;
			}
		}
		
		public virtual void Grow(Func<int, TNode> createNode = null)
		{
			// Default creation pattern.
			createNode ??= _ => Activator.CreateInstance<TNode>();
			
			// Create children array if it doesn't exist.
			children ??= new TNode[4];
			
			// Create a node for each child.
			for (int index = 0; index < 4; index++)
			{
				// Create node.
				TNode node = createNode(index);
				
				// Set values.
				node.index = index;
				node.depth = depth + 1;
				node.parent = (TNode)this;
				node.root = root ?? (TNode)this;
				
				// Set node size and position.
				node.width = width / 2;
				node.height = height / 2;
				node._x = _x + node.width * (float)Math.Floor(index % 2f) - node.width / 2;
				node._y = _y + node.height * (float)Math.Floor(index / 2f) - node.height / 2;
				
				// Insert child node.
				children[index] = node;
				
				// Invoke event.
				OnNodeAdded?.Invoke(node);
			}
		}
		
		public virtual void Shrink()
		{
			// Dispose of all child nodes.
			foreach (TNode child in children)
				child?.Dispose();
			
			// Clear children array.
			children = null;
		}

		public virtual void Dispose()
		{
			// Mark as disposed for recursion.
			IsDisposed = true;
			
			// Dispose children.
			if (children != null)
				foreach (TNode child in children)
					child?.Dispose();
			
			// Dispose of this node.
			if (!parent?.IsDisposed ?? false)
				parent.Remove(index);
		}

		public TNode[] GetNearbyNodes(int maxDepth = -1)
		{
			// Get parent's children.
			TNode target = (TNode)(root ?? this);
			
			// Get nearby nodes.
			return new[]
			{
				target.GetNode(_x, _y - height, maxDepth),
				target.GetNode(_x - width, _y, maxDepth),
				target.GetNode(_x, _y + height, maxDepth),
				target.GetNode(_x + width, _y, maxDepth),
			};
		}

		public virtual TNode GetNode(float x, float y, int maxDepth = -1)
		{
			if (IsLeaf)
				return (TNode)this;
			
			// Calculate position deltas.
			float dx = x - _x;
			float dy = y - _y;
			
			// Check bounds.
			if (!Contains(dx, dy))
				return null;
			
			// The index of the position.
			int index = GetIndex(dx, dy);
			
			// Check depth.
			if (maxDepth != -1 && depth >= maxDepth)
				return (TNode)this;
			
			// Get child node.
			TNode child = children[index];
			
			// Return child node.
			return child?.GetNode(x, y, maxDepth);
		}

		public bool Contains(float x, float y) =>
			x >= -width / 2f && x <= width / 2f && y >= -height / 2f && y <= height / 2f;
		
		public int GetIndex(float x, float y) =>
			(int)Math.Floor(x / width * 2f + 1f) + (int)Math.Floor(y / height * 2f + 1f) * 2;

		private bool Remove(int index)
		{
			// Get child.
			TNode child = children?[index];
			
			// Check if child exists.
			if (child == null) return false;
			
			// Remove child node.
			children[index] = null;
			
			// Check if all children are null and remove array.
			if (children.All(_child => _child == null))
				children = null;
			
			// Invoke event.
			OnNodeRemoved?.Invoke(child);

			return true;
		}
	}
}