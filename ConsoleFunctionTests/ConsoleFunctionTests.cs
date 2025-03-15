using ConsoleOrderExecutor.ConsoleFunction;
using ConsoleOrderExecutor.ConsoleFunction.Utils;
using ConsoleOrderExecutor.Orders.DTOs;
using ConsoleOrderExecutor.Orders.Services;
using ConsoleOrderExecutor.Products.DTOs;
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
        [Fact]
        public void ConsoleFunctions_CreateNewOrder_OrderCreationFailed()
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
            A.CallTo(() => _orderService.CreateOrder(A<CreateOrder>.Ignored)).Returns(false);

            //Act
            consoleFunctions.CreateNewOrder();

            //Assert
            Assert.DoesNotContain("Error:", strWriter.ToString());
            Assert.Contains("Failed to create order.", strWriter.ToString());
        }
        [Fact]
        public void ConsoleFunctions_CreateNewOrder_ProductNameIsNull()
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
            A.CallTo(() => _productService.ProductExist(A<string>.Ignored)).Returns(false);
            string? nameOut = null;
            A.CallTo(() => _consoleUtils.GetParameter("Please write product name.", A<Predicate<string?>>.Ignored, out nameOut)).Returns(false);
            string? priceOut = "20.5";
            A.CallTo(() => _consoleUtils.GetParameter("Please write product price.", A<Predicate<string?>>.Ignored, out priceOut)).Returns(false);

            //Act
            consoleFunctions.CreateNewOrder();

            //Assert
            A.CallTo(() => _orderService.CreateOrder(A<CreateOrder>.Ignored)).MustNotHaveHappened();
            Assert.Contains("Error: Product name is null.", strWriter.ToString());
            Assert.DoesNotContain("Product added.", strWriter.ToString());
        }
        [Fact]
        public void ConsoleFunctions_CreateNewOrder_ProductCreationFailed()
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
            A.CallTo(() => _productService.ProductExist(A<string>.Ignored)).Returns(false);
            string? nameOut = "name";
            A.CallTo(() => _consoleUtils.GetParameter("Please write product name.", A<Predicate<string?>>.Ignored, out nameOut)).Returns(false);
            string? priceOut = "20.5";
            A.CallTo(() => _consoleUtils.GetParameter("Please write product price.", A<Predicate<string?>>.Ignored, out priceOut)).Returns(false);
            A.CallTo(() => _productService.CreateProduct(A<CreateProduct>.Ignored)).Returns(false);

            //Act
            consoleFunctions.CreateNewOrder();

            //Assert
            A.CallTo(() => _orderService.CreateOrder(A<CreateOrder>.Ignored)).MustNotHaveHappened();
            Assert.Contains("Error: Could not add product.", strWriter.ToString());
            Assert.DoesNotContain("Product added.", strWriter.ToString());
        }
        [Fact]
        public void ConsoleFunctions_ShowProducts_ProductCountLessThan5_ProductShown()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            var products = new List<GetProduct> { 
                new()
                {
                    Id = 1,
                    Ean = "123",
                    Name = "name"
                }
            };
            A.CallTo(() => _productService.GetProducts()).Returns(products);
            var product = products[0];
            var consoleResult = $"id: {product.Id} ean: {product.Ean} name: {product.Name}";

            //Act
            consoleFunctions.ShowProducts();

            //Assert
            Assert.Contains(consoleResult, strWriter.ToString());
            Assert.DoesNotContain("Error: Found null product.", strWriter.ToString());
            Assert.DoesNotContain("If you want to exit write exit. If you want to see more click enter or write anything.", strWriter.ToString());
        }
        [Fact]
        public void ConsoleFunctions_ShowProducts_ProductCountGreaterThan5_ProductShown()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            var strReader = new StringReader(Environment.NewLine);
            Console.SetIn(strReader);
            var products = A.CollectionOfFake<GetProduct>(6);
            A.CallTo(() => _productService.GetProducts()).Returns(products);
            var consoleResult = "If you want to exit write exit. If you want to see more click enter or write anything.";

            //Act
            consoleFunctions.ShowProducts();

            //Assert
            Assert.Contains(consoleResult, strWriter.ToString());
            Assert.DoesNotContain("Error: Found null product.", strWriter.ToString());
        }
        [Fact]
        public void ConsoleFunctions_ShowProducts_ProductCountGreaterThan5_UserExited_ProductShown()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            var strReader = new StringReader("exit");
            Console.SetIn(strReader);
            var products = A.CollectionOfFake<GetProduct>(6);
            A.CallTo(() => _productService.GetProducts()).Returns(products);
            var consoleResult = "If you want to exit write exit. If you want to see more click enter or write anything.";

            //Act
            consoleFunctions.ShowProducts();

            //Assert
            Assert.Contains(consoleResult, strWriter.ToString());
            Assert.DoesNotContain("Error: Found null product.", strWriter.ToString());
            Assert.DoesNotContain("End of products.", strWriter.ToString());
        }
        [Fact]
        public void ConsoleFunctions_ShowOrders_OrderCountLessThan5_OrdersShown()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            var orders = new List<GetOrder> {
                new()
                {
                    Id = 1,
                    OrderValue = 20.5m,
                    Products =
                    [
                        new GetOrderProduct()
                        {
                            Id = 1,
                            Name = "name",
                            Ean = "123",
                            Price = 20.5m
                        }
                    ],
                    OrderType = "Company",
                    DeliveryAddress = "address",
                    StatusName = "Nowe",
                    PaymentOption = "Karta"
                }
            };
            A.CallTo(() => _orderService.GetOrders()).Returns(orders);
            var order = orders[0];
            var consoleOrderResult =
                $"id: {order.Id} value: {order.OrderValue} PLN status: {order.StatusName} payment option: {order.PaymentOption}" + Environment.NewLine +
                $"type: {order.OrderType} address: {order.DeliveryAddress}" + Environment.NewLine +
                "Products:";
            var prod = order.Products.Select(x => $"id: {x.Id} ean: {x.Ean} name: {x.Name} price: {x.Price}");
            var consoleProdResult = String.Join("\\n", prod);

            //Act
            consoleFunctions.ShowOrders();

            //Assert
            Assert.Contains(consoleOrderResult, strWriter.ToString());
            Assert.Contains(consoleProdResult, strWriter.ToString());
            Assert.DoesNotContain("Error: Found null order.", strWriter.ToString());
            Assert.DoesNotContain("If you want to exit write exit. If you want to see more click enter or write anything.", strWriter.ToString());
        }
        [Fact]
        public void ConsoleFunctions_ShowOrders_OrderCountGreaterThan5_OrdersShown()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            var strReader = new StringReader(Environment.NewLine);
            Console.SetIn(strReader);
            var orders = A.CollectionOfFake<GetOrder>(6);
            A.CallTo(() => _orderService.GetOrders()).Returns(orders);
            var consoleResult = "If you want to exit write exit. If you want to see more click enter or write anything.";

            //Act
            consoleFunctions.ShowOrders();

            //Assert
            Assert.Contains(consoleResult, strWriter.ToString());
            Assert.DoesNotContain("Error: Found null order.", strWriter.ToString());
        }
        [Fact]
        public void ConsoleFunctions_ShowOrders_OrderCountGreaterThan5_UserExited_OrdersShown()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            var strReader = new StringReader("exit");
            Console.SetIn(strReader);
            var orders = A.CollectionOfFake<GetOrder>(6);
            A.CallTo(() => _orderService.GetOrders()).Returns(orders);
            var consoleResult = "If you want to exit write exit. If you want to see more click enter or write anything.";

            //Act
            consoleFunctions.ShowOrders();

            //Assert
            Assert.Contains(consoleResult, strWriter.ToString());
            Assert.DoesNotContain("Error: Found null order.", strWriter.ToString());
            Assert.DoesNotContain("End of orders.", strWriter.ToString());
        }
        [Fact]
        public void ConsoleFunctions_SendOrder_SuccessfullyCreatedThread_WriteSuccessMessage()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            string? orderIdOut = "1";
            A.CallTo(() => _consoleUtils.GetParameter("Please write the id of the order that you want to send.", A<Predicate<string?>>.Ignored, out orderIdOut)).Returns(false);
            A.CallTo(() => _orderService.OrderExist(A<int>.Ignored)).Returns(true);
            A.CallTo(() => _orderService.GetStatusId("W magazynie")).Returns(1);
            A.CallTo(() => _orderService.GetOrderStatusId(A<int>.Ignored)).Returns(1);
            A.CallTo(() => _orderService.GetStatusId("W wysyłce")).Returns(2);

            //Act
            consoleFunctions.SendOrder();

            //Assert
            Assert.DoesNotContain("Error:", strWriter.ToString());
            Assert.Contains($"The order has been sent for shipment. Please check later if status of order {orderIdOut} has been changed.", strWriter.ToString());
        }
        [Fact]
        public void ConsoleFunctions_SendOrder_OrderDoNotExist_WriteError()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            string? orderIdOut = "1";
            A.CallTo(() => _consoleUtils.GetParameter("Please write the id of the order that you want to send.", A<Predicate<string?>>.Ignored, out orderIdOut)).Returns(false);
            A.CallTo(() => _orderService.OrderExist(A<int>.Ignored)).Returns(false);

            //Act
            consoleFunctions.SendOrder();

            //Assert
            Assert.Contains($"Error: The order with id {orderIdOut} do not exist in database.", strWriter.ToString());
            Assert.DoesNotContain($"The order has been sent for shipment. Please check later if status of order {orderIdOut} has been changed.", strWriter.ToString());
        }
        [Fact]
        public void ConsoleFunctions_SendOrder_StatusWarehouseDoNotExist_WriteError()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            string? orderIdOut = "1";
            A.CallTo(() => _consoleUtils.GetParameter("Please write the id of the order that you want to send.", A<Predicate<string?>>.Ignored, out orderIdOut)).Returns(false);
            A.CallTo(() => _orderService.OrderExist(A<int>.Ignored)).Returns(true);
            A.CallTo(() => _orderService.GetStatusId("W magazynie")).Returns(0);

            //Act
            consoleFunctions.SendOrder();

            //Assert
            Assert.Contains("Error: Could not find status with the name W magazynie", strWriter.ToString());
            Assert.DoesNotContain($"The order has been sent for shipment. Please check later if status of order {orderIdOut} has been changed.", strWriter.ToString());
        }
        [Fact]
        public void ConsoleFunctions_SendOrder_OrderHaveDifferentStatusThenWarehouse_WriteError()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            string? orderIdOut = "1";
            A.CallTo(() => _consoleUtils.GetParameter("Please write the id of the order that you want to send.", A<Predicate<string?>>.Ignored, out orderIdOut)).Returns(false);
            A.CallTo(() => _orderService.OrderExist(A<int>.Ignored)).Returns(true);
            A.CallTo(() => _orderService.GetStatusId("W magazynie")).Returns(1);
            A.CallTo(() => _orderService.GetOrderStatusId(A<int>.Ignored)).Returns(3);

            //Act
            consoleFunctions.SendOrder();

            //Assert
            Assert.Contains("Error: Cannot change given order status, because the order status is not W magazynie.", strWriter.ToString());
            Assert.DoesNotContain($"The order has been sent for shipment. Please check later if status of order {orderIdOut} has been changed.", strWriter.ToString());
        }
        [Fact]
        public void ConsoleFunctions_SendOrder_StatusSendDONotExist_WriteError()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            string? orderIdOut = "1";
            A.CallTo(() => _consoleUtils.GetParameter("Please write the id of the order that you want to send.", A<Predicate<string?>>.Ignored, out orderIdOut)).Returns(false);
            A.CallTo(() => _orderService.OrderExist(A<int>.Ignored)).Returns(true);
            A.CallTo(() => _orderService.GetStatusId("W magazynie")).Returns(1);
            A.CallTo(() => _orderService.GetOrderStatusId(A<int>.Ignored)).Returns(1);
            A.CallTo(() => _orderService.GetStatusId("W wysyłce")).Returns(0);

            //Act
            consoleFunctions.SendOrder();

            //Assert
            Assert.Contains("Error: Could not find status with the name W wysyłce", strWriter.ToString());
            Assert.DoesNotContain($"The order has been sent for shipment. Please check later if status of order {orderIdOut} has been changed.", strWriter.ToString());
        }
    }
}
