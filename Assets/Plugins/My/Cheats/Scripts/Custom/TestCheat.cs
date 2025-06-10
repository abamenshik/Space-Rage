using System.Collections;
using MyCheats;
using UnityEngine;

public class TestCheat : CheatBase
{
    public override string Name => "vbn";

    public override string Description => "просто тестовый чит";

    protected override IEnumerator DoProt()
    {
        yield break;
    }
}