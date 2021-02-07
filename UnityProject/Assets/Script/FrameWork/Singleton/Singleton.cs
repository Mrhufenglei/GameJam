//*************************************************************
//
//          Maggic@2015.6.2
//
//*************************************************************
//

public class Singleton<T> where T : class, new()
{
    private static T m_t = null;

    public static T Instance
    {
        get
        {
            if (m_t == null)
            {
                m_t = new T();
            }
            return m_t;
        }
    }
}
