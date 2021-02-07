//----------------------------------------------------------------------
//
//              Maggic @  2020/8/14 14:18:16
//
//---------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
namespace LocalModels
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseLocalModelManager
    {

        protected Dictionary<string, BaseLocalModel> m_localModels = new Dictionary<string, BaseLocalModel>();

        /// <summary>
        /// 实例化数据类
        /// </summary>
        public abstract void InitialiseLocalModels();


        #region Method Load

        /// <summary>
        /// 加载一个表格数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="callBack"></param>
        public void Load(string fileName, Action callBack)
        {
            string _folderName = System.IO.Path.Combine("Assets/_Resources/LocalModel", fileName);
            _folderName = System.IO.Path.ChangeExtension(_folderName, "bytes");
            var _handler = GameApp.Resources.LoadAssetAsync<TextAsset>(_folderName);
            _handler.Completed += (x) =>
            {
                //加载完成后 触发回调  把数据填充到
                if (_handler.Status != UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
                {
                    Debug.LogErrorFormat("<color=red>[LocalModel]</color>Load LocalModel {0} error !!!", fileName);
                    return;
                }
                BaseLocalModel _localModel = null;
                m_localModels.TryGetValue(fileName, out _localModel);
                if (_localModel == null)
                {
                    Debug.LogErrorFormat("<color=red>[LocalModel]</color>Load LocalModel {0} is null !!!", fileName);
                    return;
                }
                _localModel.Initialise(fileName, _handler.Result.bytes);
                if (callBack != null) callBack.Invoke();
            };

        }

        /// <summary>
        /// 加载所有表格数据
        /// </summary>
        /// <param name="callBack"></param>
        public void LoadAll(Action callBack)
        {
            List<string> _fileNames = m_localModels.Keys.ToList();
            Loads(_fileNames, callBack);
        }

        /// <summary>
        /// 加载多个表格数据
        /// </summary>
        /// <param name="fileNames"></param>
        /// <param name="callBack"></param>
        public void Loads(List<string> fileNames, Action callBack)
        {
            if (fileNames == null)
            {
                Debug.LogErrorFormat("<color=red>[LocalModel]</color>Load  fileNames == null");
                return;
            }
            List<object> _filePaths = new List<object>(fileNames.Count);
            for (int i = 0; i < fileNames.Count; i++)
            {
                string _path = GetFilePath(fileNames[i]);
                Debug.LogFormat("<color=red>[LocalModel]</color>fileNames[{0}] == {1}", i, _path);
                _filePaths.Add(_path);
            }

            var _handler = GameApp.Resources.LoadAssetsAsync<TextAsset>(
                _filePaths,
                (x) => { Debug.LogFormat("<color=red>[LocalModel]</color>load {0}", x.name); },
                UnityEngine.AddressableAssets.Addressables.MergeMode.Union);

            _handler.Completed += (x) =>
            {
                //加载完成后 触发回调  把数据填充到
                if (_handler.Status != UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
                {
                    Debug.LogErrorFormat("<color=red>[LocalModel]</color>Load  LocalModel {0} error !!!", _filePaths.ToString());
                    return;
                }


                for (int i = 0; i < fileNames.Count; i++)
                {
                    string _fileName = fileNames[i];
                    BaseLocalModel _localModel = null;
                    m_localModels.TryGetValue(_fileName, out _localModel);
                    if (_localModel == null)
                    {
                        Debug.LogErrorFormat("<color=red>[LocalModel]</color>Load LocalModel {0} is null !!!", _fileName);
                        return;
                    }

                    for (int s = 0; s < _handler.Result.Count; s++)
                    {
                        TextAsset _textAsset = _handler.Result[s];
                        if (string.Equals(_textAsset.name, _fileName))
                        {
                            _localModel.Initialise(_fileName, _textAsset.bytes);
                            break;
                        }
                    }
                }

                if (callBack != null) callBack.Invoke();

            };
        }

        #endregion

        private string GetFilePath(string fileName)
        {
            string _folderName = "Assets/_Resources/LocalModel/" + fileName + ".bytes";
            return _folderName;
        }
    }
}
