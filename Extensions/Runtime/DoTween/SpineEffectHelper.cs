using Cysharp.Threading.Tasks;
using FrameworkExtensions.DoTween;
using FrameworkExtensions.Mono.Color;
using UnityEngine;

/// <summary>
/// 控制spine色彩及透明度
/// </summary>
public class SpineEffectHelper
{
    public SpineEffectHelper(MeshRenderer _renderer)
    {
        renderer = _renderer;

        colorChanger = new ColorChanger(GetColor, SetColor);
        valueChanger = new ValueChanger(GetAlpha,SetAlpha);

        block = new MaterialPropertyBlock();
        SetColor(renderer.material.color);
    }

    /// <summary>
    /// 渐变颜色
    /// </summary>
    /// <param name="targetColor"></param>
    /// <param name="time"></param>
    /// <param name="endAction"></param>
    public async UniTask ChangeColor(Color targetColor, float time)
    {
        StopHelper();

       await colorChanger.Start(targetColor, time);
    }

    /// <summary>
    /// 渐变透明度
    /// </summary>
    /// <param name="targetAlpha"></param>
    /// <param name="time"></param>
    /// <param name="endAction"></param>
    public async UniTask ChangeAlpha(float targetAlpha,float time)
    {
        StopHelper();

       await valueChanger.Start(time,targetAlpha);
    }

    //色彩变化控制器
    private ColorChanger colorChanger;
    //透明度变化控制器
    private ValueChanger valueChanger;
    //材质块
    private MaterialPropertyBlock block;
    //mesh
    private MeshRenderer renderer;

    //记录当前颜色数据
    private Color currentColor;

    /// <summary>
    /// 停止所有渐变
    /// </summary>
    public void StopHelper()
    {
        valueChanger.Stop();
        colorChanger.Stop();
    }

    /// <summary>
    /// 获取透明度
    /// </summary>
    /// <returns></returns>
    public float GetAlpha()
    {
        return GetColor().a;
    }

    /// <summary>
    /// 设置透明度
    /// </summary>
    /// <param name="alpha"></param>
    public void SetAlpha(float alpha)
    {
        SetColor(GetColor().SetAlpha(alpha));
    }

    /// <summary>
    /// 获取颜色
    /// </summary>
    /// <returns></returns>
    public Color GetColor()
    {
        return currentColor;
    }


    /// <summary>
    /// 设置颜色
    /// </summary>
    /// <param name="_color"></param>
    public void SetColor(Color _color)
    {
        block.SetColor(colorTag, _color);
        renderer.SetPropertyBlock(block);
        currentColor = _color;
    }
   
    //shader变量名
    private const string colorTag = "_Color";

   
}
