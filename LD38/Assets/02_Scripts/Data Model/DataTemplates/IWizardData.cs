﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWizardData : ICharacterData
{
    int Level { get; set; }

    int Exp { get; set; }    	
}
