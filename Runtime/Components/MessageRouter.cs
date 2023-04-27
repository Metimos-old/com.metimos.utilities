using System;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Metimos
{
	[AddComponentMenu("Metimos/Utilities/Message Router")]
	public sealed class MessageRouter : MonoBehaviour
	{
		static MessageRouter()
		{
			Type type = typeof(MessageRouter);
			MethodInfo[] methods = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic);

			foreach (MethodInfo method in methods)
				s_messages.Add(method.Name);
		}
		
		public MonoBehaviour[] targets;
		public bool alwaysPropagate;
		public SendMessageOptions options = SendMessageOptions.RequireReceiver;

		private static readonly HashSet<string> s_messages = new();
		
		private readonly HashSet<string> _messages = new();
		
		public static bool IsMessage(string message) => s_messages.Contains(message);
		
		public bool AddMessage(string message) => IsMessage(message) && _messages.Add(message);
		public bool RemoveMessage(string message) => _messages.Remove(message);
		public bool ContainsMessage(string message) => _messages.Contains(message);
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Internal_Invoke(string message)
		{
			if (!alwaysPropagate && !_messages.Contains(message)) return;
			
			foreach (MonoBehaviour target in targets)
				target.SendMessage(message, options);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Internal_Invoke<T>(string message, T value)
		{
			if (!alwaysPropagate && !_messages.Contains(message)) return;
			
			foreach (MonoBehaviour target in targets)
				target.SendMessage(message, value, options);
		}
		
		private void OnCollisionEnter(Collision collision) => Internal_Invoke("OnCollisionEnter", collision);
		private void Awake() => Internal_Invoke("Awake");
		private void Reset() => Internal_Invoke("Reset");
		private void Start() => Internal_Invoke("Start");
		private void Update() => Internal_Invoke("Update");
		private void FixedUpdate() => Internal_Invoke("FixedUpdate");
		private void LateUpdate() => Internal_Invoke("LateUpdate");
		private void OnEnable() => Internal_Invoke("OnEnable");
		private void OnDisable() => Internal_Invoke("OnDisable");
		private void OnDestroy() => Internal_Invoke("OnDestroy");
		private void OnGUI() => Internal_Invoke("OnGUI");
		private void OnAnimatorIK(int layerIndex) => Internal_Invoke("OnAnimatorIK", layerIndex);
		private void OnAnimatorMove() => Internal_Invoke("OnAnimatorMove");
		private void OnApplicationFocus(bool hasFocus) => Internal_Invoke("OnApplicationFocus", hasFocus);
		private void OnApplicationPause(bool pauseStatus) => Internal_Invoke("OnApplicationPause", pauseStatus);
		private void OnApplicationQuit() => Internal_Invoke("OnApplicationQuit");
		// private void OnAudioFilterRead(float[] data, int channels) => Internal_Invoke("OnAudioFilterRead", data, channels);
		private void OnBecameInvisible() => Internal_Invoke("OnBecameInvisible");
		private void OnBecameVisible() => Internal_Invoke("OnBecameVisible");
		private void OnBeforeTransformParentChanged() => Internal_Invoke("OnBeforeTransformParentChanged");
		private void OnCanvasGroupChanged() => Internal_Invoke("OnCanvasGroupChanged");
		private void OnCanvasHierarchyChanged() => Internal_Invoke("OnCanvasHierarchyChanged");
		private void OnCollisionEnter2D(Collision2D collision) => Internal_Invoke("OnCollisionEnter2D", collision);
		private void OnCollisionExit(Collision collision) => Internal_Invoke("OnCollisionExit", collision);
		private void OnCollisionExit2D(Collision2D collision) => Internal_Invoke("OnCollisionExit2D", collision);
		private void OnCollisionStay(Collision collision) => Internal_Invoke("OnCollisionStay", collision);
		private void OnCollisionStay2D(Collision2D collision) => Internal_Invoke("OnCollisionStay2D", collision);
		private void OnConnectedToServer() => Internal_Invoke("OnConnectedToServer");
		private void OnControllerColliderHit(ControllerColliderHit hit) => Internal_Invoke("OnControllerColliderHit", hit);
		private void OnDidApplyAnimationProperties() => Internal_Invoke("OnDidApplyAnimationProperties");
		private void OnDrawGizmos() => Internal_Invoke("OnDrawGizmos");
		private void OnDrawGizmosSelected() => Internal_Invoke("OnDrawGizmosSelected");
		private void OnJointBreak(float breakForce) => Internal_Invoke("OnJointBreak", breakForce);
		private void OnJointBreak2D(Joint2D brokenJoint) => Internal_Invoke("OnJointBreak2D", brokenJoint);
		private void OnMouseDown() => Internal_Invoke("OnMouseDown");
		private void OnMouseDrag() => Internal_Invoke("OnMouseDrag");
		private void OnMouseEnter() => Internal_Invoke("OnMouseEnter");
		private void OnMouseExit() => Internal_Invoke("OnMouseExit");
		private void OnMouseOver() => Internal_Invoke("OnMouseOver");
		private void OnMouseUp() => Internal_Invoke("OnMouseUp");
		private void OnMouseUpAsButton() => Internal_Invoke("OnMouseUpAsButton");
		private void OnParticleCollision(GameObject other) => Internal_Invoke("OnParticleCollision", other);
		private void OnParticleSystemStopped() => Internal_Invoke("OnParticleSystemStopped");
		private void OnParticleTrigger() => Internal_Invoke("OnParticleTrigger");
		private void OnParticleUpdateJobScheduled() => Internal_Invoke("OnParticleUpdateJobScheduled");
		private void OnPostRender() => Internal_Invoke("OnPostRender");
		private void OnPreCull() => Internal_Invoke("OnPreCull");
		private void OnPreRender() => Internal_Invoke("OnPreRender");
		private void OnRectTransformDimensionsChange() => Internal_Invoke("OnRectTransformDimensionsChange");
		// private void OnRenderImage(RenderTexture src, RenderTexture dest) => Internal_Invoke("OnRenderImage", src, dest);
		private void OnRenderObject() => Internal_Invoke("OnRenderObject");
		private void OnServerInitialized() => Internal_Invoke("OnServerInitialized");
		private void OnTransformChildrenChanged() => Internal_Invoke("OnTransformChildrenChanged");
		private void OnTransformParentChanged() => Internal_Invoke("OnTransformParentChanged");
		private void OnTriggerEnter(Collider collider) => Internal_Invoke("OnTriggerEnter", collider);
		private void OnTriggerEnter2D(Collider2D collider) => Internal_Invoke("OnTriggerEnter2D", collider);
		private void OnTriggerExit(Collider collider) => Internal_Invoke("OnTriggerExit", collider);
		private void OnTriggerExit2D(Collider2D collider) => Internal_Invoke("OnTriggerExit2D", collider);
		private void OnTriggerStay(Collider collider) => Internal_Invoke("OnTriggerStay", collider);
		private void OnTriggerStay2D(Collider2D collider) => Internal_Invoke("OnTriggerStay2D", collider);
		// private void OnValidate() => Internal_Invoke("OnValidate");
		private void OnWillRenderObject() => Internal_Invoke("OnWillRenderObject");
	}
}
