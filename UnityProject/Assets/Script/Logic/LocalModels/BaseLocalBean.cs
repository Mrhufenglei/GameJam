using UnityEngine;
using System.Collections;
using System.Text;
using System;
using System.IO;
using System.Net;

//using resource;

namespace LocalModels
{
    public abstract class BaseLocalBean : IBeanBuilder
    {

        //protected byte[] buff;
        private short messageLength;
        private int position;
        private static readonly long t19700101 = new DateTime(1970, 1, 1, 0, 0, 0, 0).Ticks;
        private static readonly int time_factor = 10000;
        private static readonly Encoding encoding = Encoding.UTF8;
        private FileStream file;
        private byte[] raws;

        protected BaseLocalBean()
        {
            position = 0;
        }

        public abstract BaseLocalBean createBean();


        public int readFromBytes(byte[] raws, int startPos)
        {
            this.raws = raws;
            position = startPos;
            try
            {
                messageLength = readShort();
                //type = readShort();
                if (!readImpl())
                {
                    return -1;

                }

            }
            catch (Exception e)
            {
                throw e;
            }
            return position;


        }
        //		public bool readFromFile(FileStream readfile)
        //		{
        //			file = readfile;
        //			try
        //			{
        //				messageLength = readShort();
        //				//type = readShort();
        //				return readImpl();
        //				
        //			}
        //			catch(Exception e)
        //			{
        //				throw e;
        //			}
        //			
        //			
        //		}

        public int getLength()
        {
            return messageLength;

        }

        protected void readBytes(byte[] datas, int buffLength)
        {
            for (int i = 0; i < buffLength; i++, position++)
            {
                datas[i] = raws[position];

            }
            //file.Read(datas,0,buffLength);

            //file.Seek(buffLength,SeekOrigin.Current);

        }

        protected short readShort()
        {
            byte[] datas = new byte[2];
            readBytes(datas, 2);
            short s = BitConverter.ToInt16(datas, 0);
            s = IPAddress.NetworkToHostOrder(s);
            return s;
        }

        protected bool readBool()
        {
            short s = readShort();
            if (s == 1)
            {
                return true;
            }
            return false;
        }

        protected int readInt()
        {
            byte[] datas = new byte[4];
            readBytes(datas, 4);
            int i = BitConverter.ToInt32(datas, 0);
            i = IPAddress.NetworkToHostOrder(i);
            return i;

        }

        protected long readLong()
        {
            byte[] datas = new byte[8];
            readBytes(datas, 8);
            long l = BitConverter.ToInt64(datas, 0);
            l = IPAddress.NetworkToHostOrder(l);
            return l;
        }

        protected DateTime readDate()
        {
            long l = readLong();
            l *= time_factor;
            l += t19700101;
            DateTime time = new DateTime(l);
            return time;


        }

        protected float readFloat()
        {
            byte[] datas = new byte[4];
            readBytes(datas, 4);
            int i = BitConverter.ToInt32(datas, 0);
            i = IPAddress.NetworkToHostOrder(i);
            byte[] fb = BitConverter.GetBytes(i);
            float f = BitConverter.ToSingle(fb, 0);
            return f;
        }

        protected double readDouble()
        {

            byte[] datas = new byte[8];
            readBytes(datas, 8);
            long l = BitConverter.ToInt64(datas, 0);
            l = IPAddress.NetworkToHostOrder(l);
            double d = BitConverter.Int64BitsToDouble(l);
            return d;
        }


        protected string readLocalString()
        {
            short length = readShort();
            byte[] strbuff = new byte[length - 2];
            readBytes(strbuff, length - 2);
            try
            {
                return encoding.GetString(strbuff);
            }
            catch (Exception e)
            {
                Debug.Log("get string ecode error " + e.Message);
                return "";
            }
        }

        protected string readCommonString()
        {
            string temp = readLocalString();
            return toCommonString(temp);
        }


        protected string toCommonString(string key)
        {
            string str = key;// StringManager.Instance().getString(key);

            if (str == null)
            {
                return key;
            }
            return str;
        }

        protected abstract bool readImpl();


        //		public byte[] getBuff()
        //		{
        //			return buff;	
        //		}
        //		
        //		public void setBuff(byte[] inp)
        //		{
        //			buff = inp;	
        //		}

    }
}