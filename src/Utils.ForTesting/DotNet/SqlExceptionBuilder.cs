using System;
using System.Collections;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace DavidLievrouw.Utils.ForTesting.DotNet {
  /// <summary>
  /// Unit test helper class, that can build a SqlException class.
  /// </summary>
  /// <remarks>http://stackoverflow.com/questions/11976996/moq-and-throwing-a-sqlexception</remarks>
  public class SqlExceptionBuilder {
    string _errorMessage;
    int _errorNumber;

    public SqlException Build() {
      var error = CreateError();
      var errorCollection = CreateErrorCollection(error);
      return CreateException(errorCollection);
    }

    public SqlExceptionBuilder WithErrorNumber(int number) {
      _errorNumber = number;
      return this;
    }

    public SqlExceptionBuilder WithErrorMessage(string message) {
      _errorMessage = message;
      return this;
    }

    SqlError CreateError() {
      // Create instance via reflection...
      var ctors = typeof(SqlError).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
      var firstSqlErrorCtor = ctors.First(ctor => ctor.GetParameters().Count() == 7); // Need a specific constructor!
      var error = firstSqlErrorCtor.Invoke(
        new object[] {
          _errorNumber,
          new byte(),
          new byte(),
          string.Empty,
          string.Empty,
          string.Empty,
          new int()
        }) as SqlError;

      return error;
    }

    static SqlErrorCollection CreateErrorCollection(SqlError error) {
      // Create instance via reflection...
      var sqlErrorCollectionCtor = typeof(SqlErrorCollection).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
      var errorCollection = sqlErrorCollectionCtor.Invoke(new object[] { }) as SqlErrorCollection;

      // Add error...
      typeof(SqlErrorCollection).GetMethod("Add", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(errorCollection, new object[] { error });

      return errorCollection;
    }

    SqlException CreateException(IEnumerable errorCollection) {
      // Create instance via reflection...
      var ctor = typeof(SqlException).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
      var sqlException = ctor.Invoke(
        new object[] {
          // With message and error collection...
          _errorMessage,
          errorCollection,
          null,
          Guid.NewGuid()
        }) as SqlException;

      return sqlException;
    }
  }
}