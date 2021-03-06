﻿ 

using System;
using System.Linq.Expressions;

namespace Parva.Utility.Expression2Sql
{
	class BinaryExpression2Sql : BaseExpression2Sql<BinaryExpression>
	{
		private void OperatorParser(ExpressionType expressionNodeType, int operatorIndex, SqlPack sqlPack, bool useIs = false)
		{
			switch (expressionNodeType)
			{
				case ExpressionType.And:
				case ExpressionType.AndAlso:
					sqlPack.Sql.Insert(operatorIndex, "\nand");
					break;
				case ExpressionType.Equal:
					if (useIs)
					{
						sqlPack.Sql.Insert(operatorIndex, " is");
					}
					else
					{
						sqlPack.Sql.Insert(operatorIndex, " =");
					}
					break;
				case ExpressionType.GreaterThan:
					sqlPack.Sql.Insert(operatorIndex, " >");
					break;
				case ExpressionType.GreaterThanOrEqual:
					sqlPack.Sql.Insert(operatorIndex, " >=");
					break;
				case ExpressionType.NotEqual:
					if (useIs)
					{
						sqlPack.Sql.Insert(operatorIndex, " is not");
					}
					else
					{
						sqlPack.Sql.Insert(operatorIndex, " <>");
					}
					break;
				case ExpressionType.Or:
				case ExpressionType.OrElse:
					sqlPack.Sql.Insert(operatorIndex, "\nor");
					break;
				case ExpressionType.LessThan:
					sqlPack.Sql.Insert(operatorIndex, " <");
					break;
				case ExpressionType.LessThanOrEqual:
					sqlPack.Sql.Insert(operatorIndex, " <=");
					break;
				default:
					throw new NotImplementedException("未实现的节点类型" + expressionNodeType);
			}
		}

		protected override SqlPack Join(BinaryExpression expression, SqlPack sqlPack)
		{
			Expression2SqlProvider.Join(expression.Left, sqlPack);
			int operatorIndex = sqlPack.Sql.Length;

			Expression2SqlProvider.Join(expression.Right, sqlPack);
			int sqlLength = sqlPack.Sql.Length;

			if (sqlLength - operatorIndex == 5 && sqlPack.ToString().EndsWith("null"))
			{
				OperatorParser(expression.NodeType, operatorIndex, sqlPack, true);
			}
			else
			{
				OperatorParser(expression.NodeType, operatorIndex, sqlPack);
			}

			return sqlPack;
		}

		protected override SqlPack Where(BinaryExpression expression, SqlPack sqlPack)
		{
			Expression2SqlProvider.Where(expression.Left, sqlPack);
			int signIndex = sqlPack.Length;

			Expression2SqlProvider.Where(expression.Right, sqlPack);
			int sqlLength = sqlPack.Length;

			if (sqlLength - signIndex == 5 && sqlPack.ToString().EndsWith("null"))
			{
				OperatorParser(expression.NodeType, signIndex, sqlPack, true);
			}
			else
			{
				OperatorParser(expression.NodeType, signIndex, sqlPack);
			}

			return sqlPack;
		}
	}
}