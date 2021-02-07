public class EventArgLanguageType : BaseEventArgs
{
    private LanguageType m_languageType = LanguageType.English;
    public LanguageType LanguageType
    {
        get { return m_languageType; }
    }

    public void SetData(LanguageType languageType)
    {
        m_languageType = languageType;
    }
}