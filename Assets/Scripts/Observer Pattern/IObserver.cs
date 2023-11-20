using System;
using System.Collections.Generic;

public interface IObserver
{
    public void OnNotify(int Dmg, string Tag=null);
}