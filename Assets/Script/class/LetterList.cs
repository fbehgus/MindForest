using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
[Serializable]
public class Letter {
    public string content;
    public string timestamp;
}
[Serializable]
public class LetterList {
    public List<Letter> letters = new List<Letter>();
}
