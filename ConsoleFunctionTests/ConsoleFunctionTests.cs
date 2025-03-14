using ConsoleOrderExecutor.ConsoleFunction;
using ConsoleOrderExecutor.ConsoleFunction.Utils;
using ConsoleOrderExecutor.Orders.DTOs;
using ConsoleOrderExecutor.Orders.Services;
using ConsoleOrderExecutor.Products.Services;
using FakeItEasy;

namespace ConsoleOrderExecutor.Tests.ConsoleFunctionTests
{
    public class ConsoleFunctionTests
    {
        private readonly IConsoleUtils _consoleUtils;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public ConsoleFunctionTests()
        {
            _consoleUtils = A.Fake<IConsoleUtils>();
            _orderService = A.Fake<IOrderService>();
            _productService = A.Fake<IProductService>();
        }
        [Fact]
        public void ConsoleFunctions_CreateNewOrder_OrderCreated()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            string? companyOut = "1";
            A.CallTo(() => _consoleUtils.GetParameter("Is this order for company or physical person? Type 1 if yes or 0 if false.", A<Predicate<string?>>.Ignored, out companyOut)).Returns(false);
            string? addressOut = "address";
            A.CallTo(() => _consoleUtils.GetParameter("Please write delivery address (max 250 characters).", A<Predicate<string?>>.Ignored, out addressOut)).Returns(false);
            var paymentList = new List<GetPaymentOption>() {
                new() {
                    Id = 2,
                    Name = ""
                }
            };
            var paymentInfoInString = paymentList.Select(x => x.Id + " - " + x.Name);
            A.CallTo(() => _orderService.GetPaymentOptions()).Returns(paymentList);
            string? paymentOut = "2";
            A.CallTo(() => _consoleUtils.GetParameter("Please choose the payment option, by writing its number.\\n" + String.Join("\\n", paymentInfoInString), A<Predicate<string?>>.Ignored, out paymentOut)).Returns(false);
            string? eanOut = "2342343";
            A.CallTo(() => _consoleUtils.GetParameter("Please write ean of product you want to add.", A<Predicate<string?>>.Ignored, out eanOut)).Returns(false);
            A.CallTo(() => _productService.ProductExist(A<string>.Ignored)).Returns(true);
            string? priceOut = "20.5";
            A.CallTo(() => _consoleUtils.GetParameter("Please write product price.", A<Predicate<string?>>.Ignored, out priceOut)).Returns(false);
            string? nextStepOut = "done";
            A.CallTo(() => _consoleUtils.GetParameter("If you done write done, otherwise next.", A<Predicate<string?>>.Ignored, out nextStepOut)).Returns(false);
            A.CallTo(() => _orderService.CreateOrder(A<CreateOrder>.Ignored)).Returns(true);

            //Act
            consoleFunctions.CreateNewOrder();

            //Assert
            Assert.DoesNotContain("Error:", strWriter.ToString());
            Assert.Contains("Order created.", strWriter.ToString());
        }
    }
}
