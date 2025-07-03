using Application.UseCases.Config.Show;

namespace Application.Tests.Unit.UseCases.Config.Show;

[Parallelizable]
[Category("Unit")]
public sealed class RequestModelTests
{
    [Test]
    public void ConfigShowRequestModel__Equals_RequestModel__Returns_True_If_Data_Equal()
    {
        // Arrange
        var request1 = new RequestModel
        {
            Name = RequestModel.NameOptions.Datasource
        };

        var request2 = new RequestModel
        {
            Name = RequestModel.NameOptions.Datasource
        };

        // Act
        var result = request1.Equals(request2);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void ConfigShowRequestModel__Equals_RequestModel__Returns_False_If_Data_Not_Equal()
    {
        // Arrange
        var request1 = new RequestModel
        {
            Name = RequestModel.NameOptions.Unknown
        };

        var request2 = new RequestModel
        {
            Name = RequestModel.NameOptions.Datasource
        };

        // Act
        var result = request1.Equals(request2);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ConfigShowRequestModel__Equals_RequestModel__Returns_False_If_Arg_Is_Null()
    {
        // Arrange
        var request = new RequestModel
        {
            Name = RequestModel.NameOptions.Datasource
        };

        // Act
        var result = request.Equals(null);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ConfigShowRequestModel__Equals_RequestModel__Returns_True_If_Arg_Is_Same_Reference()
    {
        // Arrange
        var request = new RequestModel
        {
            Name = RequestModel.NameOptions.Datasource
        };

        // Act
        var result = request.Equals(request);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void ConfigShowRequestModel__Equals_object__Returns_False_If_Not_Same_Reference()
    {
        // Arrange
        var request1 = new RequestModel
        {
            Name = RequestModel.NameOptions.Datasource
        };

        var request2 = new
        {
            Name = RequestModel.NameOptions.Datasource
        };

        // Act
        var result = request1.Equals(request2);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ConfigShowRequestModel__GetHasCode__Returns_Same_Value_For_Same_Data()
    {
        // Arrange
        var request1 = new RequestModel
        {
            Name = RequestModel.NameOptions.Datasource
        };

        var request2 = new RequestModel
        {
            Name = RequestModel.NameOptions.Datasource
        };

        // Act
        var result1 = request1.GetHashCode();
        var result2 = request2.GetHashCode();

        // Assert
        Assert.That(result1, Is.EqualTo(result2));
    }

    [Test]
    public void ConfigShowRequestModel__GetHashCode__Returns_Difference_Value_If_Name_Is_Different()
    {
        // Arrange
        var request1 = new RequestModel
        {
            Name = RequestModel.NameOptions.Datasource
        };

        var request2 = new RequestModel
        {
            Name = RequestModel.NameOptions.Unknown
        };

        // Act
        var result1 = request1.GetHashCode();
        var result2 = request2.GetHashCode();

        // Assert
        Assert.That(result1, Is.Not.EqualTo(result2));
    }
}