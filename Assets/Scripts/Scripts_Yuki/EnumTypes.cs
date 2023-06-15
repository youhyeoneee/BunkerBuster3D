using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnumTypes 
{
    public enum GimmickType
    {
        // HorizontalSizeUp, // 두께 증가
        // HorizontalSizeDown, // 두께 감소
        // VerticalSizeUp, // 길이 증가 
        // VerticalSizeDown, // 길이 감소
        // Devolve, // 퇴화

        // ==========
        Evolve, // 진화
        Drilled, // 뚫리는 
        Undrilled, // 안뚫리는 
        Drill, // 먹는 드릴
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
