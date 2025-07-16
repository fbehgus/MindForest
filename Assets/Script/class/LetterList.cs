using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
[Serializable]
public class SerializedLetter {
    public string content;
    public string timestamp;
}
[Serializable]
public class LetterList {
    public List<SerializedLetter> letters = new List<SerializedLetter>();
}
