using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnumTypes 
{
    public enum GimmickType
    {
        HorizontalSizeUp, // 두께 증가
        HorizontalSizeDown, // 두께 감소
        VerticalSizeUp, // 길이 증가 
        VerticalSizeDown, // 길이 감소
        Evolve, // 진화
        Devolve // 퇴화
    }

    public enum TagType
    {
        Player,
        Walltmp,
        ExplosionObject,
        Drill,
    }

    public enum GameStateType
    {
        Intro,
        Ready,
        Playing,
        Ending,
        BreakingCubes,
        Finished
    }

    public enum Animtype
    {
        End,
        Evolve,
    }
}
