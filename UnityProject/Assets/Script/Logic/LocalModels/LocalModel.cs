using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Net;
namespace LocalModels
{
    public abstract class LocalModel<T, Key> where T : LocalBean
    {
        private IList<T> beans = new List<T>();
        private Dictionary<Key, T> beansMap = new Dictionary<Key, T>();

        protected string GetDbFileName(string filename)
        {
            return "LocalModel/" + filename;
            //return filename;
        }
        public LocalModel()
        {
            initialise();
        }
        protected abstract BeanBuilder GetBuilder();

        protected abstract Key GetBeanKey(T bean);


        protected virtual void ArrangeElements()
        {
            foreach (T ele in beans)
            {
                Key k = GetBeanKey(ele);
                beansMap[k] = ele;

            }
        }



        public void initialise()
        {
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
            BeanBuilder builder = GetBuilder();

            string filename = GetDbFileName(builder.GetFilename());
            try
            {
                TextAsset fileres = Resources.Load<TextAsset>(filename);
                //byte[] raws = Globals.serverinfo.GetConfigINfoByName(filename.ToLower());
                byte[] raws = fileres.bytes;
                if (raws == null)
                {
                    Debug.LogError("Config file:" + filename + " not Find");
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
                        Debug.LogError("read resource file failed :: " + filename);
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