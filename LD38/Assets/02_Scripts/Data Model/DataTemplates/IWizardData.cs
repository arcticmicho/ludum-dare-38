using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWizardData : ICharacterData
{
    string WizardID { get; set; }

    int Exp { get; set; }    	
}
