using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Net;
namespace LocalModels
{
    public abstract class BaseLocalModelImpl<T, Key> where T : BaseLocalBean
    {
        private string fileName;
        private byte[] fileBytes;
        private IList<T> beans = new List<T>();
        private Dictionary<Key, T> beansMap = new Dictionary<Key, T>();

        protected abstract IBeanBuilder GetBuilder();

        protected abstract Key GetBeanKey(T bean);


        protected virtual void ArrangeElements()
        {
            foreach (T ele in beans)
            {
                Key k = GetBeanKey(ele);
                beansMap[k] = ele;

            }
        }

        public void Initialise(string name, byte[] bytes)
        {
            fileName = name;
            fileBytes = bytes;
            ReadFromFile();
            ArrangeElements();
        }

        public IList<T> GetAllElement()
        {
            return beans;
        }

        public T GetElementById(Key key)
        {
            T bean = null;
            if (beansMap.TryGetValue(key, out bean))
            {
                return bean;

            }
            Debug.LogWarning(this.GetType() + " find element Error key = " + key.ToString());
            return null;
        }

        protected bool ReadFromFile()
        {

            //Debug.Log("read from resource file");
            IBeanBuilder builder = GetBuilder();
            try
            {
                byte[] raws = fileBytes;
                if (raws == null)
                {
                    Debug.LogError("Config file:" + fileName + " not Find");
                    return false;
                }
                int count = GetElementCount(raws);
                int startPos = 4;
                for (int i = 0; i < count; i++)
                {
                    T ele = (T)builder.createBean();
                    startPos = ele.readFromBytes(raws, startPos);
                    if (startPos < 0)
                    {
                        Debug.LogError("read resource file failed :: " + fileName);
                    }
                    beans.Add(ele);
                }
                //Debug.Log("read from resource complete");
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);

                return false;
            }
            finally
            {

            }
            return true;

        }


        protected int GetElementCount(byte[] raws)
        {

            byte[] datas = new byte[4];
            datas[0] = raws[0];
            datas[1] = raws[1];
            datas[2] = raws[2];
            datas[3] = raws[3];

            //fs.Seek(4,SeekOrigin.Current);
            int count = BitConverter.ToInt32(datas, 0);
            count = IPAddress.NetworkToHostOrder(count);
            return count;
        }

    }
}