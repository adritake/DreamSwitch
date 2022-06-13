using UnityEngine;

public class DreamLevel: Singleton<DreamLevel>
{
    public DreamNumber level;
}

public enum DreamNumber{ Dream1, Dream2, Dream3, Dream4, Dream5 }