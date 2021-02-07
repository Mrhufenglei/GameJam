using UnityEngine;
using System.Collections;
namespace LocalModels
{
    public interface IBeanBuilder
    {
        BaseLocalBean createBean();
    }
}