using Newtonsoft.Json;
using System;

[Serializable]
public class EdebiCubeSide
{
    [JsonProperty("question")]
    public string Question;

    [JsonProperty("answer")]
    public string Answer;
}

public class EdebiCube
{
    public EdebiCubeSide Blue;
    public EdebiCubeSide Purple;
    public EdebiCubeSide Orange;
    public EdebiCubeSide Red;
    public EdebiCubeSide Green;
    public EdebiCubeSide Pink;
}