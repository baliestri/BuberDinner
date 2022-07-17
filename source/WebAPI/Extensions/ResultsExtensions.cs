// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using BuberDinner.Core.Common.Errors;
using ErrorOr;

namespace BuberDinner.WebAPI.Extensions;

public static class ResultsExtensions {
  public static IResult Errors(this IResultExtensions results, IEnumerable<Error> errors) {
    Error firstError = errors.First();

    int statusCode = firstError.Type switch {
      ErrorType.Conflict => StatusCodes.Status409Conflict,
      ErrorType.Validation => StatusCodes.Status400BadRequest,
      ErrorType.NotFound => StatusCodes.Status404NotFound,
      (ErrorType)CustomErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
      _ => StatusCodes.Status500InternalServerError
    };

    Dictionary<string, object?> extensions = new() { { "code", firstError.Code } };

    return Results.Problem(firstError.Description, statusCode: statusCode, extensions: extensions);
  }
}
