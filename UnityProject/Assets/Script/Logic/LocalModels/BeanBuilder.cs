using UnityEngine;
using System.Collections;
namespace LocalModels
{
    public interface BeanBuilder
    {
        LocalBean createBean();
        string GetFilename();
    }
}