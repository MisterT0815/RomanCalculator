namespace frontend.Services
{
    public interface ICalculationService
    {
        void Initialize();
        Task<string> Calculate(CalculationModel calculation);
    }
}