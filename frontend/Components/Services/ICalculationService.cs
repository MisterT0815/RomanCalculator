namespace frontend.Services
{
    public interface ICalculationService
    {
        void Initialize();
        Task<int> Calculate(CalculationModel calculation);
    }
}