
namespace MinCurvMethod
{
///<summary>
/// Можем выбрать конкретный вид весовой функции для интерполяции методом усреднения значений:
/// прямая зависимость от расстояния, обратная зависимость, обратная квадратичная и обратная 4-ая степень
///</summary>
    public enum WeightFunction { Linear, Inverse, Inverse2, Inverse4 }
}
