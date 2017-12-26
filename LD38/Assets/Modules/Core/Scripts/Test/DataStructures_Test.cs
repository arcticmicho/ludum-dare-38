using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStructures_Test : MonoBehaviour
{
    public class DataClass
    {
        public float A;

        public DataClass(float a)
        {
            A = a;
        }
    }

    public class DataClass2
    {
        public float posX;
        public float posY;

        public float velX;
        public float velY;

        public float posX2;
        public float posY2;

        public float posX1;
        public float posY2d;

        public float velX3;
        public float velY4;

        public float posX25;
        public float posY26;

        public float posX198;
        public float posY276;

        public float velX354;
        public float velY432;

        public float posX2512;
        public float posY2611;

    }

    public struct StructData
    {
        public float posX;
        public float posY;

        public float velX;
        public float velY;

        public float posX2;
        public float posY2;

        public float posX1;
        public float posY2d;

        public float velX3;
        public float velY4;

        public float posX25;
        public float posY26;

        public float posX198;
        public float posY276;

        public float velX354;
        public float velY432;

        public float posX2512;
        public float posY2611;
    }


    // Use this for initialization
    void Start ()
    {
        RunTests();
	}


    public void RunTests()
    {
       // UpdateListTests();
       ListLoopTest();
    }

    #region List Update Test
    public void UpdateListTests()
    {
        int stressCount = 5000;

        UpdateList<DataClass> _updateListA = new UpdateList<DataClass>(1000,1000);

        for (int a = 0; a < stressCount; a++)
        {
            _updateListA.Add(new DataClass(a));
        }

        List<DataClass> _updateListB = new List<DataClass>();

        for (int a = 0; a < stressCount; a++)
        {
            _updateListB.Add(new DataClass(a));
        }

        List<DataClass> _updateListC = new List<DataClass>();

        for (int a = 0; a < stressCount; a++)
        {
            _updateListC.Add(new DataClass(a));
        }

        DataClass[] _updateListD = new DataClass[stressCount];
        int _size = 5000;
        for (int a = 0; a < stressCount; a++)
        {
            _updateListD[a] = new DataClass(a);
        }


        int updates = 0;
        int iteratorSafe = stressCount * 2;

        DebugTools.Tik();

        while (_updateListA.Count > 0)
        {
            var elements = _updateListA.Elements;
            for (int a = 0; a < _updateListA.Count; ++a)
            {
                elements[a].A--;
                updates++;
                if (elements[a].A < 0)
                {
                    a = _updateListA.RemoveAt(a);
                }
            }

            iteratorSafe--;
            if (iteratorSafe < 0)
            {
                Debug.LogWarning("Leave By Deadlock");
                break;
            }
        }

        DebugTools.Tok("_updateListA: Update List Class");
        Debug.LogWarning("Updates: " + updates);

        updates = 0;
        iteratorSafe = stressCount*2;

        DebugTools.Tik();
        while (_updateListB.Count > 0)
        {
            for (int a = 0; a < _updateListB.Count; ++a)
            {
                // _updateListB[a]=new DataClass(_updateListB[a].A - 1);
                 _updateListB[a].A--;
                updates++;
                if (_updateListB[a].A < 0)
                {
                    _updateListB.RemoveAt(a);
                    a--;
                }
            }

            iteratorSafe--;
            if (iteratorSafe < 0)
            {
                Debug.LogWarning("Leave By Deadlock");
                break;
            }
        }

        DebugTools.Tok("_updateListB: List with remove at");
        Debug.LogWarning("Updates: " + updates);


        updates = 0;
        iteratorSafe = stressCount * 2;

        DebugTools.Tik();
        while (_updateListC.Count > 0)
        {
            int count = _updateListC.Count;
            for (int a = 0; a < count; ++a)
            {
               // _updateListC[a] = new DataClass(_updateListC[a].A - 1);
                _updateListC[a].A--;
                updates++;
                if (_updateListC[a].A < 0)
                {
                    _updateListC[a] = _updateListC[count - 1];
                    _updateListC.RemoveAt(a);
                    a--;
                    count--;
                }
            }

            iteratorSafe--;
            if (iteratorSafe < 0)
            {
                Debug.LogWarning("Leave By Deadlock");
                break;
            }
        }

        DebugTools.Tok("_updateListC: List With Swap Delete");
        Debug.LogWarning("Updates: " + updates);

        updates = 0;
        iteratorSafe = stressCount * 2;

        DebugTools.Tik();
        while (_size > 0)
        {
            for (int a = 0; a < _size; ++a)
            {
                _updateListD[a].A--;
                updates++;
                if (_updateListD[a].A < 0)
                {
                    _updateListD[a] = _updateListD[_size - 1];
                    a--;
                    _size--;
                }
            }

            iteratorSafe--;
            if (iteratorSafe < 0)
            {
                Debug.LogWarning("Leave By Deadlock");
                break;
            }
        }

        DebugTools.Tok("_updateListD: Plain Array with Swap Delete");
        Debug.LogWarning("Updates: " + updates);


        Debug.LogWarning("UpdateListTests End");
    }

    #endregion

    #region List Loop Test

    public void ListLoopTest()
    {
        int stressCount = 5000000;

        DataClass2[] _list = new DataClass2[stressCount];
        StructData[] _list2 = new StructData[stressCount];

        float[] _velX = new float[stressCount];
        float[] _velY = new float[stressCount];
        float[] _posX = new float[stressCount];
        float[] _posY = new float[stressCount];
        float[] _posX2 = new float[stressCount];
        float[] _posY2 = new float[stressCount];

        for (int a = 0; a < stressCount; a++)
        {
            _list[a] = new DataClass2();
            _list2[a] = new StructData();

            _velX[a] = 0;
            _velY[a] = 0;
            _posX[a] = 0;
            _posY[a] = 0;
            _posX2[a] = 0;
            _posY2[a] = 0;
        }

        DebugTools.Tik();

        for (int a = 0; a < stressCount; a++)
        {
            _list[a].posX = _list[a].posY * 2;
            _list[a].velX = _list[a].velY * 2;
            _list[a].posX2 = _list[a].posY2 * 2;
        }

        DebugTools.Tok("Normal");

        DebugTools.Tik();

        for (int a = 0; a < stressCount; a++)
        {
            _list2[a].posX = _list2[a].posY * 2;
            _list2[a].velX = _list2[a].velY * 2;
            _list2[a].posX2 = _list2[a].posY2 * 2;
        }

        DebugTools.Tok("Normal Struct");

        DebugTools.Tik();

        for (int a = 0; a < stressCount; a++)
        {
            _posX[a] = _posY[a] * 2;
            _velX[a] = _velY[a] * 2;
            _posX2[a] = _posY2[a] * 2;
        }

        DebugTools.Tok("Multi Array");


        DebugTools.Tik();

        for (int a = 0; a < stressCount; a++)
        {
            _posX[a] = _posY[a] * 2;
        }

        for (int a = 0; a < stressCount; a++)
        {
            _velX[a] = _velY[a] * 2;
        }
        for (int a = 0; a < stressCount; a++)
        {
            _posX2[a] = _posY2[a] * 2;
        }
        DebugTools.Tok("Multi For");
    }
    #endregion

}
