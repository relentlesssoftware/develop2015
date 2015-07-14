using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Relentless
{
	/// <summary>
	/// Provider Manager.
	/// 
	/// This is the main entry point for your game code to use the specific services implemented by the providers.
	/// It uses aggregation to call each provider instance on this same GameObject that supports the current platform.
	/// 
	/// You can choose to aggregate to all providers, or only the frst one, or the higest priority provider. This depends on your specific situation and needs.
	/// 
	/// To get the Highest priority provider use the method GetHighestPriorityProvider()
	///  
	/// Note: Place all the provider implementations on the same gameobject as this ProviderManager, and do not call them directly.
	/// </summary>
	public abstract class ProviderManager<T> : MonoBehaviour
		where T : ProviderBase
	{
		private readonly List<T> m_providers = new List<T>();

		#region Properties

		public bool IsReady { get; protected set; }

		#endregion

		#region Unity Methods

		public void Awake()
		{
			StartCoroutine(CoroutineInitialise());
		}

		#endregion

		#region Construction and Initialisation

		protected ProviderManager()
		{
			IsReady = false;
		}

		private IEnumerator CoroutineInitialise()
		{
			Debug.Log("ProviderManager: Initialising providers for " + gameObject.name);

			m_providers.Clear();
			
			OnBeforeInitialise();

			foreach (var provider in gameObject.GetComponents<T>())
			{
				if (provider.IsValid())
				{
					this.m_providers.Add(provider);

					provider.Initialise();
				}
				else
				{
					// remove this provider as its not valid
					// this will prevent any "OnGui" updates and other Unity events form being sent to it
					GameObject.Destroy(provider);
				}
			}

			if (HasProviders())
			{
				Debug.Log(String.Format("{0}: ProviderManager: Initialised {1} providers, waiting for IsReady", gameObject.name, m_providers.Count), this);

				// Wait for any providers to become ready. Some might need to log into online services, or wait for use rauthentication.
				bool allProdersAreReady = false;
				while (!allProdersAreReady)
				{
					yield return null;

					allProdersAreReady = true;
					foreach (var provider in AllProviders())
					{
						if (!provider.IsReady)
						{
							allProdersAreReady = false;
							break;
						}
					}
				}
			}
			else
			{
				Debug.LogError(String.Format("{0}: ProviderManager: No providers for this platform!", gameObject.name), this);
			}

			Debug.Log(String.Format("{0}: ProviderManager: IsReady", gameObject.name, m_providers.Count), this);

			OnAfterInitialise();

			IsReady = true;

			yield return null;
		}

		/// <summary>
		/// This is called when before any providers are intialised
		/// </summary>
		protected virtual void OnBeforeInitialise()
		{
			
		}

		/// <summary>
		/// This is called when all providers are ready
		/// </summary>
		protected virtual void OnAfterInitialise()
		{

		}

		#endregion

		#region Helper Methods

		protected bool HasProviders()
		{
			return m_providers.Count > 0;
		}

		protected IEnumerable<T> AllProviders()
		{
			foreach (T provider in m_providers)
			{
				yield return provider;
			}
		}

		protected T GetHighestPriorityProvider()
		{
			T providerFound = null;

			foreach(var provider in m_providers)
			{
				if(providerFound == null)
				{
					providerFound = provider;
				}
				else if (providerFound.ProviderPriority > provider.ProviderPriority)
				{
					providerFound = provider;
				}
			}

			return providerFound;
		}

		#endregion
	}
}
