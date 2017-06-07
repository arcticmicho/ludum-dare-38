using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameplayStateContainer
{
    static class PerType<T> where T : GameplayState<Wizard,TransitionData>
    {
        public static T typeobject;
    }

    public static T Get<T>() where T : GameplayState<Wizard, TransitionData>
    {
        return PerType<T>.typeobject;
    }

    public static void Set<T>(T newObject) where T : GameplayState<Wizard, TransitionData>
    {
        PerType<T>.typeobject = newObject;
    }


}
