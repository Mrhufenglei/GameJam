using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class ResourcesManager : MonoBehaviour
{
    /// <summary>
    /// The Instance Provider used by the Addressables System.
    /// </summary>
    public IInstanceProvider InstanceProvider { get { return Addressables.InstanceProvider; } }

    /// <summary>
    /// Used to resolve a string using addressables config values
    /// </summary>
    public string ResolveInternalId(string id)
    {
        return Addressables.ResolveInternalId(id);
    }

    /// <summary>
    /// Functor to transform internal ids before being used by the providers.
    /// See the [TransformInternalId](../manual/TransformInternalId.html) documentation for more details.
    /// </summary>
    public Func<IResourceLocation, string> InternalIdTransformFunc
    {
        get { return Addressables.InternalIdTransformFunc; }
        set { Addressables.InternalIdTransformFunc = value; }
    }

    /// <summary>
    /// The subfolder used by the Addressables system for its initialization data.
    /// </summary>
    public string StreamingAssetsSubFolder { get { return Addressables.StreamingAssetsSubFolder; } }

    /// <summary>
    /// The path used by the Addressables system for its initialization data.
    /// </summary>
    public string BuildPath { get { return Addressables.BuildPath; } }

    /// <summary>
    /// The path that addressables player data gets copied to during a player build.
    /// </summary>
    public string PlayerBuildDataPath { get { return Addressables.PlayerBuildDataPath; } }

    /// <summary>
    /// The path used by the Addressables system to load initialization data.
    /// </summary>
    public string RuntimePath { get { return Addressables.RuntimePath; } }

    /// <summary>
    /// Initialize Addressables system.  Addressables will be initialized on the first API call if this is not called explicitly.
    /// See the [InitializeAsync](../manual/InitializeAsync.html) documentation for more details.
    /// </summary>
    /// <returns>The operation handle for the request.</returns>
    public AsyncOperationHandle<IResourceLocator> InitializeAsync()
    {
        return Addressables.InitializeAsync();
    }

    /// <summary>
    /// Additively load catalogs from runtime data.  In order for content catalog caching to work properly the catalog json file
    /// should have a .hash file associated with the catalog.  This hash file will be used to determine if the catalog
    /// needs to be updated or not.  If no .hash file is provided, the catalog will be loaded from the specified path every time.
    /// </summary>
    /// <param name="catalogPath">The path to the runtime data.</param>
    /// <param name="providerSuffix">This value, if not null or empty, will be appended to all provider ids loaded from this data.</param>
    /// <returns>The operation handle for the request.</returns>
    public AsyncOperationHandle<IResourceLocator> LoadContentCatalogAsync(string catalogPath, string providerSuffix = null)
    {
        return Addressables.LoadContentCatalogAsync(catalogPath, false, providerSuffix);
    }

    /// <summary>
    /// Additively load catalogs from runtime data.  In order for content catalog caching to work properly the catalog json file
    /// should have a .hash file associated with the catalog.  This hash file will be used to determine if the catalog
    /// needs to be updated or not.  If no .hash file is provided, the catalog will be loaded from the specified path every time.
    /// </summary>
    /// <param name="catalogPath">The path to the runtime data.</param>
    /// <param name="autoReleaseHandle">If true, the async operation handle will be automatically released on completion.</param>
    /// <param name="providerSuffix">This value, if not null or empty, will be appended to all provider ids loaded from this data.</param>
    /// <returns>The operation handle for the request.</returns>
    public AsyncOperationHandle<IResourceLocator> LoadContentCatalogAsync(string catalogPath, bool autoReleaseHandle, string providerSuffix = null)
    {
        return Addressables.LoadContentCatalogAsync(catalogPath, autoReleaseHandle, providerSuffix);
    }

    /// <summary>
    /// Load a single asset
    /// See the [Loading Addressable Assets](../manual/LoadingAddressableAssets.html) documentation for more details.
    /// </summary>
    /// <param name="location">The location of the asset.</param>
    public AsyncOperationHandle<TObject> LoadAssetAsync<TObject>(IResourceLocation location)
    {
        return Addressables.LoadAssetAsync<TObject>(location);
    }

    /// <summary>
    /// Load a single asset
    /// See the [Loading Addressable Assets](../manual/LoadingAddressableAssets.html) documentation for more details.
    /// </summary>
    /// <param name="key">The key of the location of the asset.</param>
    public AsyncOperationHandle<TObject> LoadAssetAsync<TObject>(object key)
    {
        return Addressables.LoadAssetAsync<TObject>(key);
    }

    /// <summary>
    /// Loads the resource locations specified by the keys.
    /// The method will always return success, with a valid IList of results. If nothing matches keys, IList will be empty
    /// </summary>
    /// <param name="keys">The set of keys to use.</param>
    /// <param name="mode">The mode for merging the results of the found locations.</param>
    /// <param name="type">A type restriction for the lookup.  Only locations of the provided type (or derived type) will be returned.</param>
    /// <returns>The operation handle for the request.</returns>
    public AsyncOperationHandle<IList<IResourceLocation>> LoadResourceLocationsAsync(IList<object> keys, Addressables.MergeMode mode, Type type = null)
    {
        return Addressables.LoadResourceLocationsAsync(keys, mode, type);
    }

    /// <summary>
    /// Request the locations for a given key.
    /// The method will always return success, with a valid IList of results. If nothing matches key, IList will be empty
    /// </summary>
    /// <param name="key">The key for the locations.</param>
    /// <param name="type">A type restriction for the lookup.  Only locations of the provided type (or derived type) will be returned.</param>
    /// <returns>The operation handle for the request.</returns>
    public AsyncOperationHandle<IList<IResourceLocation>> LoadResourceLocationsAsync(object key, Type type = null)
    {
        return Addressables.LoadResourceLocationsAsync(key, type);
    }

    /// <summary>
    /// Load multiple assets, based on list of locations provided.
    /// If any fail, all successful loads and dependencies will be released.  The returned .Result will be null, and .Status will be Failed.
    /// See the [Loading Addressable Assets](../manual/LoadingAddressableAssets.html) documentation for more details.
    /// </summary>
    /// <param name="locations">The locations of the assets.</param>
    /// <param name="callback">Callback Action that is called per load operation.</param>
    /// <returns>The operation handle for the request.</returns>
    public AsyncOperationHandle<IList<TObject>> LoadAssetsAsync<TObject>(IList<IResourceLocation> locations, Action<TObject> callback)
    {
        return Addressables.LoadAssetsAsync(locations, callback, true);
    }

    /// <summary>
    /// Load multiple assets, based on list of locations provided.
    /// See the [Loading Addressable Assets](../manual/LoadingAddressableAssets.html) documentation for more details.
    /// </summary>
    /// <param name="locations">The locations of the assets.</param>
    /// <param name="callback">Callback Action that is called per load operation.</param>
    /// <param name="releaseDependenciesOnFailure">
    /// If all matching locations succeed, this parameter is ignored.
    /// When true, if any matching location fails, all loads and dependencies will be released.  The returned .Result will be null, and .Status will be Failed.
    /// When false, if any matching location fails, the returned .Result will be an IList of size equal to the number of locations attempted.  Any failed location will
    /// correlate to a null in the IList, while successful loads will correlate to a TObject in the list. The .Status will still be Failed.
    /// When true, op does not need to be released if anything fails, when false, it must always be released.
    /// </param>
    /// <returns>The operation handle for the request.</returns>
    public AsyncOperationHandle<IList<TObject>> LoadAssetsAsync<TObject>(IList<IResourceLocation> locations, Action<TObject> callback, bool releaseDependenciesOnFailure)
    {
        return Addressables.LoadAssetsAsync(locations, callback, releaseDependenciesOnFailure);
    }

    /// <summary>
    /// Load multiple assets.
    /// Each key in the provided list will be translated into a list of locations.  Those many lists will be combined
    /// down to one based on the provided MergeMode.
    /// If any locations from the final list fail, all successful loads and dependencies will be released.  The returned
    /// .Result will be null, and .Status will be Failed.
    /// See the [Loading Addressable Assets](../manual/LoadingAddressableAssets.html) documentation for more details.
    /// </summary>
    /// <param name="keys">List of keys for the locations.</param>
    /// <param name="callback">Callback Action that is called per load operation.</param>
    /// <param name="mode">Method for merging the results of key matches.  See <see cref="MergeMode"/> for specifics</param>
    /// <returns>The operation handle for the request.</returns>
    public AsyncOperationHandle<IList<TObject>> LoadAssetsAsync<TObject>(IList<object> keys, Action<TObject> callback, Addressables.MergeMode mode)
    {
        return Addressables.LoadAssetsAsync(keys, callback, mode, true);
    }

    /// <summary>
    /// Load multiple assets.
    /// Each key in the provided list will be translated into a list of locations.  Those many lists will be combined
    /// down to one based on the provided MergeMode.
    /// See the [Loading Addressable Assets](../manual/LoadingAddressableAssets.html) documentation for more details.
    /// </summary>
    /// <param name="keys">List of keys for the locations.</param>
    /// <param name="callback">Callback Action that is called per load operation.</param>
    /// <param name="mode">Method for merging the results of key matches.  See <see cref="MergeMode"/> for specifics</param>
    /// <param name="releaseDependenciesOnFailure">
    /// If all matching locations succeed, this parameter is ignored.
    /// When true, if any matching location fails, all loads and dependencies will be released.  The returned .Result will be null, and .Status will be Failed.
    /// When false, if any matching location fails, the returned .Result will be an IList of size equal to the number of locations attempted.  Any failed location will
    /// correlate to a null in the IList, while successful loads will correlate to a TObject in the list. The .Status will still be Failed.
    /// When true, op does not need to be released if anything fails, when false, it must always be released.
    /// </param>
    /// <returns>The operation handle for the request.</returns>
    public AsyncOperationHandle<IList<TObject>> LoadAssetsAsync<TObject>(IList<object> keys, Action<TObject> callback, Addressables.MergeMode mode, bool releaseDependenciesOnFailure)
    {
        return Addressables.LoadAssetsAsync(keys, callback, mode, releaseDependenciesOnFailure);
    }

    /// <summary>
    /// Load all assets that match the provided key.
    /// If any fail, all successful loads and dependencies will be released.  The returned .Result will be null, and .Status will be Failed.
    /// See the [Loading Addressable Assets](../manual/LoadingAddressableAssets.html) documentation for more details.
    /// </summary>
    /// <param name="key">Key for the locations.</param>
    /// <param name="callback">Callback Action that is called per load operation (per loaded asset).</param>
    /// <returns>The operation handle for the request.</returns>
    public AsyncOperationHandle<IList<TObject>> LoadAssetsAsync<TObject>(object key, Action<TObject> callback)
    {
        return Addressables.LoadAssetsAsync(key, callback, true);
    }

    /// <summary>
    /// Load all assets that match the provided key.
    /// See the [Loading Addressable Assets](../manual/LoadingAddressableAssets.html) documentation for more details.
    /// </summary>
    /// <param name="key">Key for the locations.</param>
    /// <param name="callback">Callback Action that is called per load operation (per loaded asset).</param>
    /// <param name="releaseDependenciesOnFailure">
    /// If all matching locations succeed, this parameter is ignored.
    /// When true, if any matching location fails, all loads and dependencies will be released.  The returned .Result will be null, and .Status will be Failed.
    /// When false, if any matching location fails, the returned .Result will be an IList of size equal to the number of locations attempted.  Any failed location will
    /// correlate to a null in the IList, while successful loads will correlate to a TObject in the list. The .Status will still be Failed.
    /// When true, op does not need to be released if anything fails, when false, it must always be released.
    /// </param>
    /// <returns>The operation handle for the request.</returns>
    public AsyncOperationHandle<IList<TObject>> LoadAssetsAsync<TObject>(object key, Action<TObject> callback, bool releaseDependenciesOnFailure)
    {
        return Addressables.LoadAssetsAsync(key, callback, releaseDependenciesOnFailure);
    }

    /// <summary>
    /// Release asset.
    /// </summary>
    /// <typeparam name="TObject">The type of the object being released</typeparam>
    /// <param name="obj">The asset to release.</param>
    public void Release<TObject>(TObject obj)
    {
        Addressables.Release(obj);
    }

    /// <summary>
    /// Release the operation and its associated resources.
    /// </summary>
    /// <typeparam name="TObject">The type of the AsyncOperationHandle being released</typeparam>
    /// <param name="handle">The operation handle to release.</param>
    public void Release<TObject>(AsyncOperationHandle<TObject> handle)
    {
        Addressables.Release(handle);
    }

    /// <summary>
    /// Release the operation and its associated resources.
    /// </summary>
    /// <param name="handle">The operation handle to release.</param>
    public void Release(AsyncOperationHandle handle)
    {
        Addressables.Release(handle);
    }

    /// <summary>
    /// Releases and destroys an object that was created via Addressables.InstantiateAsync.
    /// </summary>
    /// <param name="instance">The GameObject instance to be released and destroyed.</param>
    /// <returns>Returns true if the instance was successfully released.</returns>
    public bool ReleaseInstance(GameObject instance)
    {
        return Addressables.ReleaseInstance(instance);
    }

    /// <summary>
    /// Releases and destroys an object that was created via Addressables.InstantiateAsync.
    /// </summary>
    /// <param name="handle">The handle to the game object to destroy, that was returned by InstantiateAsync.</param>
    /// <returns>Returns true if the instance was successfully released.</returns>
    public bool ReleaseInstance(AsyncOperationHandle handle)
    {
       return Addressables.ReleaseInstance(handle);
    }

    /// <summary>
    /// Releases and destroys an object that was created via Addressables.InstantiateAsync.
    /// </summary>
    /// <param name="handle">The handle to the game object to destroy, that was returned by InstantiateAsync.</param>
    /// <returns>Returns true if the instance was successfully released.</returns>
    public bool ReleaseInstance(AsyncOperationHandle<GameObject> handle)
    {
        return Addressables.ReleaseInstance(handle);
    }

    /// <summary>
    /// Determines the required download size, dependencies included, for the specified <paramref name="key"/>.
    /// Cached assets require no download and thus their download size will be 0.  The Result of the operation
    /// is the download size in bytes.
    /// </summary>
    /// <returns>The operation handle for the request.</returns>
    /// <param name="key">The key of the asset(s) to get the download size of.</param>
    public AsyncOperationHandle<long> GetDownloadSizeAsync(object key)
    {
        return Addressables.GetDownloadSizeAsync(key);
    }

    /// <summary>
    /// Determines the required download size, dependencies included, for the specified <paramref name="keys"/>.
    /// Cached assets require no download and thus their download size will be 0.  The Result of the operation
    /// is the download size in bytes.
    /// </summary>
    /// <returns>The operation handle for the request.</returns>
    /// <param name="keys">The keys of the asset(s) to get the download size of.</param>
    public AsyncOperationHandle<long> GetDownloadSizeAsync(IList<object> keys)
    {
        return Addressables.GetDownloadSizeAsync(keys);
    }

    /// <summary>
    /// Downloads dependencies of assets marked with the specified label or address.
    /// </summary>
    /// <param name="key">The key of the asset(s) to load dependencies for.</param>
    /// <param name="autoReleaseHandle">Automatically releases the handle on completion</param>
    /// <returns>The AsyncOperationHandle for the dependency load.</returns>
    public AsyncOperationHandle DownloadDependenciesAsync(object key, bool autoReleaseHandle = false)
    {
        return Addressables.DownloadDependenciesAsync(key, autoReleaseHandle);
    }

    /// <summary>
    /// Downloads dependencies of assets at given locations.
    /// </summary>
    /// <param name="locations">The locations of the assets.</param>
    /// <param name="autoReleaseHandle">Automatically releases the handle on completion</param>
    /// <returns>The AsyncOperationHandle for the dependency load.</returns>
    public AsyncOperationHandle DownloadDependenciesAsync(IList<IResourceLocation> locations, bool autoReleaseHandle = false)
    {
        return Addressables.DownloadDependenciesAsync(locations, autoReleaseHandle);
    }

    /// <summary>
    /// Downloads dependencies of assets marked with the specified labels or addresses.
    /// </summary>
    /// <param name="keys">List of keys for the locations.</param>
    /// <param name="mode">Method for merging the results of key matches.  See <see cref="MergeMode"/> for specifics</param>
    /// <param name="autoReleaseHandle">Automatically releases the handle on completion</param>
    /// <returns>The AsyncOperationHandle for the dependency load.</returns>
    public AsyncOperationHandle DownloadDependenciesAsync(IList<object> keys, Addressables.MergeMode mode, bool autoReleaseHandle = false)
    {
        return Addressables.DownloadDependenciesAsync(keys, mode, autoReleaseHandle);
    }

    /// <summary>
    /// Clear the cached AssetBundles for a given key.  Operation may be performed async if Addressables
    /// is initializing or updating.
    /// </summary>
    /// <param name="key">The key to clear the cache for.</param>
    public void ClearDependencyCacheAsync(object key)
    {
        Addressables.ClearDependencyCacheAsync(key);
    }

    /// <summary>
    /// Clear the cached AssetBundles for a list of Addressable locations.  Operation may be performed async if Addressables
    /// is initializing or updating.
    /// </summary>
    /// <param name="locations">The locations to clear the cache for.</param>
    public void ClearDependencyCacheAsync(IList<IResourceLocation> locations)
    {
        Addressables.ClearDependencyCacheAsync(locations);
    }

    /// <summary>
    /// Clear the cached AssetBundles for a list of Addressable keys.  Operation may be performed async if Addressables
    /// is initializing or updating.
    /// </summary>
    /// <param name="keys">The keys to clear the cache for.</param>
    public void ClearDependencyCacheAsync(IList<object> keys)
    {
        Addressables.ClearDependencyCacheAsync(keys);
    }

    /// <summary>
    /// Instantiate a single object. Note that the dependency loading is done asynchronously, but generally the actual instantiate is synchronous.
    /// </summary>
    /// <param name="location">The location of the Object to instantiate.</param>
    /// <param name="parent">Parent transform for instantiated object.</param>
    /// <param name="instantiateInWorldSpace">Option to retain world space when instantiated with a parent.</param>
    /// <param name="trackHandle">If true, Addressables will track this request to allow it to be released via the result object.</param>
    /// <returns>The operation handle for the request.</returns>
    public AsyncOperationHandle<GameObject> InstantiateAsync(IResourceLocation location, Transform parent = null, bool instantiateInWorldSpace = false, bool trackHandle = true)
    {
        return Addressables.InstantiateAsync(location, new InstantiationParameters(parent, instantiateInWorldSpace), trackHandle);
    }

    /// <summary>
    /// Instantiate a single object. Note that the dependency loading is done asynchronously, but generally the actual instantiate is synchronous.
    /// </summary>
    /// <param name="location">The location of the Object to instantiate.</param>
    /// <param name="position">The position of the instantiated object.</param>
    /// <param name="rotation">The rotation of the instantiated object.</param>
    /// <param name="parent">Parent transform for instantiated object.</param>
    /// <param name="trackHandle">If true, Addressables will track this request to allow it to be released via the result object.</param>
    /// <returns>The operation handle for the request.</returns>
    public AsyncOperationHandle<GameObject> InstantiateAsync(IResourceLocation location, Vector3 position, Quaternion rotation, Transform parent = null, bool trackHandle = true)
    {
        return Addressables.InstantiateAsync(location, position, rotation, parent, trackHandle);
    }

    /// <summary>
    /// Instantiate a single object. Note that the dependency loading is done asynchronously, but generally the actual instantiate is synchronous.
    /// </summary>
    /// <param name="key">The key of the location of the Object to instantiate.</param>
    /// <param name="parent">Parent transform for instantiated object.</param>
    /// <param name="instantiateInWorldSpace">Option to retain world space when instantiated with a parent.</param>
    /// <param name="trackHandle">If true, Addressables will track this request to allow it to be released via the result object.</param>
    /// <returns>The operation handle for the request.</returns>
    public AsyncOperationHandle<GameObject> InstantiateAsync(object key, Transform parent = null, bool instantiateInWorldSpace = false, bool trackHandle = true)
    {
        return Addressables.InstantiateAsync(key, parent, instantiateInWorldSpace, trackHandle);
    }

    /// <summary>
    /// Instantiate a single object. Note that the dependency loading is done asynchronously, but generally the actual instantiate is synchronous.
    /// </summary>
    /// <param name="key">The key of the location of the Object to instantiate.</param>
    /// <param name="position">The position of the instantiated object.</param>
    /// <param name="rotation">The rotation of the instantiated object.</param>
    /// <param name="parent">Parent transform for instantiated object.</param>
    /// <param name="trackHandle">If true, Addressables will track this request to allow it to be released via the result object.</param>
    /// <returns>The operation handle for the request.</returns>
    public AsyncOperationHandle<GameObject> InstantiateAsync(object key, Vector3 position, Quaternion rotation, Transform parent = null, bool trackHandle = true)
    {
        return Addressables.InstantiateAsync(key, position, rotation, parent, trackHandle);
    }

    /// <summary>
    /// Instantiate a single object. Note that the dependency loading is done asynchronously, but generally the actual instantiate is synchronous.
    /// </summary>
    /// <param name="key">The key of the location of the Object to instantiate.</param>
    /// <param name="instantiateParameters">Parameters for instantiation.</param>
    /// <param name="trackHandle">If true, Addressables will track this request to allow it to be released via the result object.</param>
    /// <returns>The operation handle for the request.</returns>
    public AsyncOperationHandle<GameObject> InstantiateAsync(object key, InstantiationParameters instantiateParameters, bool trackHandle = true)
    {
        return Addressables.InstantiateAsync(key, instantiateParameters, trackHandle);
    }

    /// <summary>
    /// Instantiate a single object. Note that the dependency loading is done asynchronously, but generally the actual instantiate is synchronous.
    /// </summary>
    /// <param name="location">The location of the Object to instantiate.</param>
    /// <param name="instantiateParameters">Parameters for instantiation.</param>
    /// <param name="trackHandle">If true, Addressables will track this request to allow it to be released via the result object.</param>
    /// <returns>The operation handle for the request.</returns>
    public AsyncOperationHandle<GameObject> InstantiateAsync(IResourceLocation location, InstantiationParameters instantiateParameters, bool trackHandle = true)
    {
        return Addressables.InstantiateAsync(location, instantiateParameters, trackHandle);
    }

    /// <summary>
    /// Checks all updatable content catalogs for a new version.
    /// </summary>
    /// <param name="autoReleaseHandle">If true, the handle will automatically be released when the operation completes.</param>
    /// <returns>The operation containing the list of catalog ids that have an available update.  This can be used to filter which catalogs to update with the UpdateContent.</returns>
    public AsyncOperationHandle<List<string>> CheckForCatalogUpdates(bool autoReleaseHandle = true)
    {
        return Addressables.CheckForCatalogUpdates(autoReleaseHandle);
    }

    /// <summary>
    /// Update the specified catalogs.
    /// </summary>
    /// <param name="catalogs">The set of catalogs to update.  If null, all catalogs that have an available update will be updated.</param>
    /// <param name="autoReleaseHandle">If true, the handle will automatically be released when the operation completes.</param>
    /// <returns>The operation with the list of updated content catalog data.</returns>
    public AsyncOperationHandle<List<IResourceLocator>> UpdateCatalogs(IEnumerable<string> catalogs = null, bool autoReleaseHandle = true)
    {
        return Addressables.UpdateCatalogs(catalogs, autoReleaseHandle);
    }

    /// <summary>
    /// Add a resource locator.
    /// </summary>
    /// <param name="locator">The locator object.</param>
    /// <param name="localCatalogHash">The hash of the local catalog. This can be null if the catalog cannot be updated.</param>
    /// <param name="remoteCatalogLocation">The location of the remote catalog. This can be null if the catalog cannot be updated.</param>
    public void AddResourceLocator(IResourceLocator locator, string localCatalogHash = null, IResourceLocation remoteCatalogLocation = null)
    {
        Addressables.AddResourceLocator(locator, localCatalogHash, remoteCatalogLocation);
    }

    /// <summary>
    /// Remove a locator;
    /// </summary>
    /// <param name="locator">The locator to remove.</param>
    public void RemoveResourceLocator(IResourceLocator locator)
    {
        Addressables.RemoveResourceLocator(locator);
    }

    /// <summary>
    /// Remove all locators.
    /// </summary>
    public void ClearResourceLocators()
    {
        Addressables.ClearResourceLocators();
    }
}
