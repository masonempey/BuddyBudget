using BuddyBudget.Database;
using BuddyBudget.Database.Repositories;
using BuddyBudget.Drawables;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace BuddyBudget.Entity
{
	public class BudgetViewModel : INotifyPropertyChanged
	{

		//Used Outside resources (Youtube) to help build these Getters and Setters.

		private string _userName;
		public string UserName
		{
			get => _userName;
			set
			{
				if (_userName != value)
				{
					_userName = value;
					OnPropertyChanged(nameof(UserName));
				}
			}
		}

		private double _originalMonthlyBudget;
		public double OriginalMonthlyBudget
		{
			get => _originalMonthlyBudget;
			set
			{
				if (_originalMonthlyBudget != value)
				{
					_originalMonthlyBudget = value;
					OnPropertyChanged(nameof(OriginalMonthlyBudget));
				}
			}
		}

		private double _remainingMonthlyBudget;

		public double RemainingMonthlyBudget
		{
			get => _remainingMonthlyBudget;
			set
			{
				if (_remainingMonthlyBudget != value)
				{

					_remainingMonthlyBudget = value;

					OnPropertyChanged(nameof(RemainingMonthlyBudget));
				}
			}
		}

		private double _originalHousingBudget;
		public double OriginalHousingBudget
		{
			get => _originalHousingBudget;
			set
			{
				if (_originalHousingBudget != value)
				{
					_originalHousingBudget = value;
					OnPropertyChanged(nameof(OriginalHousingBudget));
				}
			}
		}

		private double _remainingHousingBudget;
		public double RemainingHousingBudget
		{
			get => _remainingHousingBudget;
			set
			{
				if (_remainingHousingBudget != value)
				{
					_remainingHousingBudget = value;
					OnPropertyChanged(nameof(RemainingHousingBudget));
				}
			}
		}

		private double _originalUtilitiesBudget;
		public double OriginalUtilitiesBudget
		{
			get => _originalUtilitiesBudget;
			set
			{
				if (_originalUtilitiesBudget != value)
				{
					_originalUtilitiesBudget = value;
					OnPropertyChanged(nameof(OriginalUtilitiesBudget));
				}
			}
		}

		private double _remainingUtilitiesBudget;
		public double RemainingUtilitiesBudget
		{
			get => _remainingUtilitiesBudget;
			set
			{
				if (_remainingUtilitiesBudget != value)
				{
					_remainingUtilitiesBudget = value;
					OnPropertyChanged(nameof(RemainingUtilitiesBudget));
				}
			}
		}

		private double _originalGroceriesBudget;
		public double OriginalGroceriesBudget
		{
			get => _originalGroceriesBudget;
			set
			{
				if (_originalGroceriesBudget != value)
				{
					_originalGroceriesBudget = value;
					OnPropertyChanged(nameof(OriginalGroceriesBudget));
				}
			}
		}

		private double _remainingGroceriesBudget;
		public double RemainingGroceriesBudget
		{
			get => _remainingGroceriesBudget;
			set
			{
				if (_remainingGroceriesBudget != value)
				{
					_remainingGroceriesBudget = value;
					OnPropertyChanged(nameof(RemainingGroceriesBudget));
				}
			}
		}

		private double _originalTransportationBudget;
		public double OriginalTransportationBudget
		{
			get => _originalTransportationBudget;
			set
			{
				if (_originalTransportationBudget != value)
				{
					_originalTransportationBudget = value;
					OnPropertyChanged(nameof(OriginalTransportationBudget));
				}
			}
		}

		private double _remainingTransportationBudget;
		public double RemainingTransportationBudget
		{
			get => _remainingTransportationBudget;
			set
			{
				if (_remainingTransportationBudget != value)
				{
					_remainingTransportationBudget = value;
					OnPropertyChanged(nameof(RemainingTransportationBudget));
				}
			}
		}

		private double _originalEntertainmentBudget;
		public double OriginalEntertainmentBudget
		{
			get => _originalEntertainmentBudget;
			set
			{
				if (_originalEntertainmentBudget != value)
				{
					_originalEntertainmentBudget = value;
					OnPropertyChanged(nameof(OriginalEntertainmentBudget));
				}
			}
		}

		private double _remainingEntertainmentBudget;
		public double RemainingEntertainmentBudget
		{
			get => _remainingEntertainmentBudget;
			set
			{
				if (_remainingEntertainmentBudget != value)
				{
					_remainingEntertainmentBudget = value;
					OnPropertyChanged(nameof(RemainingEntertainmentBudget));
				}
			}
		}

		private double _originalMiscellaneousBudget;
		public double OriginalMiscellaneousBudget
		{
			get => _originalMiscellaneousBudget;
			set
			{
				if (_originalMiscellaneousBudget != value)
				{
					_originalMiscellaneousBudget = value;
					OnPropertyChanged(nameof(OriginalMiscellaneousBudget));
				}
			}
		}

		private double _remainingMiscellaneousBudget;
		public double RemainingMiscellaneousBudget
		{
			get => _remainingMiscellaneousBudget;
			set
			{
				if (_remainingMiscellaneousBudget != value)
				{
					_remainingMiscellaneousBudget = value;
					OnPropertyChanged(nameof(RemainingMiscellaneousBudget));
				}
			}
		}

		private double _originalSavingsBudget;
		public double OriginalSavingsBudget
		{
			get => _originalSavingsBudget;
			set
			{
				if (_originalSavingsBudget != value)
				{
					_originalSavingsBudget = value;
					OnPropertyChanged(nameof(OriginalSavingsBudget));
				}
			}
		}

		private double _remainingSavingsBudget;
		public double RemainingSavingsBudget
		{
			get => _remainingSavingsBudget;
			set
			{
				if (_remainingSavingsBudget != value)
				{
					_remainingSavingsBudget = value;
					OnPropertyChanged(nameof(RemainingSavingsBudget));
				}
			}
		}

		private double _originalDebtBudget;
		public double OriginalDebtBudget
		{
			get => _originalDebtBudget;
			set
			{
				if (_originalDebtBudget != value)
				{
					_originalDebtBudget = value;
					OnPropertyChanged(nameof(OriginalDebtBudget));
				}
			}
		}

		private double _remainingDebtBudget;
		public double RemainingDebtBudget
		{
			get => _remainingDebtBudget;
			set
			{
				if (_remainingDebtBudget != value)
				{
					_remainingDebtBudget = value;
					OnPropertyChanged(nameof(RemainingDebtBudget));
				}
			}
		}

		private bool _isViewLayoutVisible;
		public bool IsViewLayoutVisible
		{
			get => _isViewLayoutVisible;
			set
			{
				if (_isViewLayoutVisible != value)
				{
					_isViewLayoutVisible = value;
					OnPropertyChanged(nameof(IsViewLayoutVisible));
				}
			}
		}


		private bool _isOptionsLayoutVisible;
		public bool IsOptionsLayoutVisible
		{
			get => _isOptionsLayoutVisible;
			set
			{
				if (_isOptionsLayoutVisible != value)
				{
					_isOptionsLayoutVisible = value;
					OnPropertyChanged(nameof(IsOptionsLayoutVisible));
				}
			}
		}

		private bool _isBudgetOptionScreenVisible;
		public bool IsBudgetOptionScreenVisible
		{
			get => _isBudgetOptionScreenVisible;
			set
			{
				if (_isBudgetOptionScreenVisible != value)
				{
					Debug.WriteLine(IsBudgetOptionScreenVisible);

					_isBudgetOptionScreenVisible = value;
					OnPropertyChanged(nameof(IsBudgetOptionScreenVisible));
				}
			}
		}

		private double _housingHeight;
		public double HousingHeight
		{
			get => _housingHeight;
			set
			{
				if (_housingHeight != value)
				{
					_housingHeight = value;
					OnPropertyChanged(nameof(HousingHeight));
				}
			}
		}

		private double _utilitiesHeight;
		public double UtilitiesHeight
		{
			get => _utilitiesHeight;
			set
			{
				if (_utilitiesHeight != value)
				{
					_utilitiesHeight = value;
					OnPropertyChanged(nameof(UtilitiesHeight));
				}
			}
		}

		private double _groceriesHeight;
		public double GroceriesHeight
		{
			get => _groceriesHeight;
			set
			{
				if (_groceriesHeight != value)
				{
					_groceriesHeight = value;
					OnPropertyChanged(nameof(GroceriesHeight));
				}
			}
		}

		private double _transportationHeight;
		public double TransportationHeight
		{
			get => _transportationHeight;
			set
			{
				if (_transportationHeight != value)
				{
					_transportationHeight = value;
					OnPropertyChanged(nameof(TransportationHeight));
				}
			}
		}

		private double _entertainmentHeight;
		public double EntertainmentHeight
		{
			get => _entertainmentHeight;
			set
			{
				if (_entertainmentHeight != value)
				{
					_entertainmentHeight = value;
					OnPropertyChanged(nameof(EntertainmentHeight));
				}
			}
		}

		private double _miscHeight;
		public double MiscHeight
		{
			get => _miscHeight;
			set
			{
				if (_miscHeight != value)
				{
					_miscHeight = value;
					OnPropertyChanged(nameof(MiscHeight));
				}
			}
		}

		private double _savingsHeight;
		public double SavingsHeight
		{
			get => _savingsHeight;
			set
			{
				if (_savingsHeight != value)
				{
					_savingsHeight = value;
					OnPropertyChanged(nameof(SavingsHeight));
				}
			}
		}

		private double _debtHeight;
		public double DebtHeight
		{
			get => _debtHeight;
			set
			{
				if (_debtHeight != value)
				{
					_debtHeight = value;
					OnPropertyChanged(nameof(DebtHeight));
				}
			}
		}

		private double _setHousingHeight;
		public double SetHousingHeight
		{
			get => _setHousingHeight;
			set
			{
				if (_setHousingHeight != value)
				{
					Debug.WriteLine("SetHousingHeight: " + SetHousingHeight);

					Debug.WriteLine("SetHousingHeight assigned value: " + value);
					_setHousingHeight = value;

					OnPropertyChanged(nameof(SetHousingHeight));
				}
			}
		}

		private double _setUtilitiesHeight;
		public double SetUtilitiesHeight
		{
			get => _setUtilitiesHeight;
			set
			{
				if (_setUtilitiesHeight != value)
				{
					_setUtilitiesHeight = value;
					OnPropertyChanged(nameof(SetUtilitiesHeight));
				}
			}
		}

		private double _setGroceriesHeight;
		public double SetGroceriesHeight
		{
			get => _setGroceriesHeight;
			set
			{
				if (_setGroceriesHeight != value)
				{
					_setGroceriesHeight = value;
					OnPropertyChanged(nameof(SetGroceriesHeight));
				}
			}
		}

		private double _setTransportationHeight;
		public double SetTransportationHeight
		{
			get => _setTransportationHeight;
			set
			{
				if (_setTransportationHeight != value)
				{
					_setTransportationHeight = value;
					OnPropertyChanged(nameof(SetTransportationHeight));
				}
			}
		}

		private double _setEntertainmentHeight;
		public double SetEntertainmentHeight
		{
			get => _setEntertainmentHeight;
			set
			{
				if (_setEntertainmentHeight != value)
				{
					_setEntertainmentHeight = value;
					OnPropertyChanged(nameof(SetEntertainmentHeight));
				}
			}
		}

		private double _setMiscHeight;
		public double SetMiscHeight
		{
			get => _setMiscHeight;
			set
			{
				if (_setMiscHeight != value)
				{
					_setMiscHeight = value;
					OnPropertyChanged(nameof(SetMiscHeight));
				}
			}
		}

		private double _setSavingHeight;
		public double SetSavingsHeight
		{
			get => _setSavingHeight;
			set
			{
				if (_setSavingHeight != value)
				{
					_setSavingHeight = value;
					OnPropertyChanged(nameof(SetSavingsHeight));
				}
			}
		}

		private double _setDebtHeight;
		public double SetDebtHeight
		{
			get => _setDebtHeight;
			set
			{
				if (_setDebtHeight != value)
				{
					_setDebtHeight = value;
					OnPropertyChanged(nameof(SetDebtHeight));
				}
			}
		}

		private bool _isBudgetViewVisible;
		public bool IsBudgetViewVisible
		{
			get => _isBudgetViewVisible;
			set
			{
				if (_isBudgetViewVisible != value)
				{
					_isBudgetViewVisible = value;
					OnPropertyChanged(nameof(IsBudgetViewVisible));
				}
			}
		}

		private bool _isAddExpenseLayoutVisible;
		public bool IsAddExpenseLayoutVisible
		{
			get => _isAddExpenseLayoutVisible;
			set
			{
				if (_isAddExpenseLayoutVisible != value)
				{
					_isAddExpenseLayoutVisible = value;
					OnPropertyChanged(nameof(IsAddExpenseLayoutVisible));
				}
			}
		}

		private string _connectionString;

		public string ConnectionString
		{
			get { return _connectionString; }
			set
			{
				if (_connectionString != value)
				{
					_connectionString = value;
					OnPropertyChanged(ConnectionString);
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private readonly DatabaseManager databaseManager;
		private readonly BudgetHome budgetHome;
		private readonly BudgetRepository budgetRepository;
		private readonly BudgetViewModel viewModel;
		private readonly GraphDrawViewModel graphDrawViewModel;

		public BudgetViewModel(string connectionString, string userName, BudgetHome budgetHome)
		{
			this.budgetHome = budgetHome;
			this.viewModel = this;
			_userName = userName;
			IsViewLayoutVisible = false;
			IsOptionsLayoutVisible = false;

			databaseManager = new DatabaseManager();
			budgetRepository = new BudgetRepository(connectionString, userName);
			graphDrawViewModel = new GraphDrawViewModel(userName);

			CheckAndDisplayBudget(userName);
		}

		public void SaveExpense(double housingExpense, double utilitiesExpense, double groceriesExpense, double transportationExpense, double entertainmentExpense, double miscellaneousExpense, double savingsExpense, double debtExpense)
		{
			try
			{
				string userName = _userName;
				double totalExpenses = housingExpense + utilitiesExpense + groceriesExpense + transportationExpense + entertainmentExpense + miscellaneousExpense + savingsExpense + debtExpense;

				Budget currentBudget = budgetRepository.ReturnUsersBudgetByName(userName);
				int UserId = databaseManager.GetUserIdFromDatabase(userName);

				if (currentBudget != null)
				{
					double TotalBudget = CalculateTotalBudget(currentBudget);
					double remainingBudget = TotalBudget - totalExpenses;

					if (remainingBudget >= 0)
					{
						double remainingHousingBudget = currentBudget.HousingBudget - housingExpense;
						double remainingUtilitiesBudget = currentBudget.UtilitiesBudget - utilitiesExpense;
						double remainingGroceriesBudget = currentBudget.GroceriesBudget - groceriesExpense;
						double remainingTransportationBudget = currentBudget.TransportationBudget - transportationExpense;
						double remainingEntertainmentBudget = currentBudget.EntertainmentBudget - entertainmentExpense;
						double remainingMiscellaneousBudget = currentBudget.MiscellaneousBudget - miscellaneousExpense;
						double remainingSavingsBudget = currentBudget.SavingsBudget - savingsExpense;
						double remainingDebtBudget = currentBudget.DebtBudget - debtExpense;

						

						Budget updatedBudget = new Budget
						{
							UserName = userName,
							RemainingBudget = remainingBudget,
							HousingBudget = remainingHousingBudget,
							UtilitiesBudget = remainingUtilitiesBudget,
							GroceriesBudget = remainingGroceriesBudget,
							TransportationBudget = remainingTransportationBudget,
							EntertainmentBudget = remainingEntertainmentBudget,
							MiscellaneousBudget = remainingMiscellaneousBudget,
							SavingsBudget = remainingSavingsBudget,
							DebtBudget = remainingDebtBudget,
							BudgetGraphScale = currentBudget.BudgetGraphScale,
							UserId = UserId
						};

						budgetRepository.UpdateBudget(updatedBudget);

						budgetHome.ResetValues();
						UpdateCurrentGraph();
					}
					else
					{
						Debug.WriteLine("Error: Expense exceeds budget");
						budgetHome.DisplayAlert("Budget Exceeds", "The Expenses you have entered are larger than your budget!", "OK");
					}
				}
				else
				{
					Debug.WriteLine("Error: Budget not found");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error on saving your expense In ViewModel", ex.Message);
			}
		}


		public void UpdateCurrentGraph()
		{
			try
			{
				Budget currentBudget = budgetRepository.ReturnUsersBudgetByName(_userName);

				double scaleFactor = budgetRepository.RetrieveBudgetGraphScale(_userName);

				UpdateGraphForBudgetComponent("Housing", currentBudget.HousingBudget, scaleFactor);
				UpdateGraphForBudgetComponent("Utilities", currentBudget.UtilitiesBudget, scaleFactor);
				UpdateGraphForBudgetComponent("Groceries", currentBudget.GroceriesBudget, scaleFactor);
				UpdateGraphForBudgetComponent("Transportation", currentBudget.TransportationBudget, scaleFactor);
				UpdateGraphForBudgetComponent("Entertainment", currentBudget.EntertainmentBudget, scaleFactor);
				UpdateGraphForBudgetComponent("Miscellaneous", currentBudget.MiscellaneousBudget, scaleFactor);
				UpdateGraphForBudgetComponent("Savings", currentBudget.SavingsBudget, scaleFactor);
				UpdateGraphForBudgetComponent("Debt", currentBudget.DebtBudget, scaleFactor);
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error updating current graph: {ex.Message}");
				throw;
			}
		}

		private void UpdateGraphForBudgetComponent(string componentName, double budgetValue, double scaleFactor)
		{
			double barHeight = budgetValue * scaleFactor;

			switch (componentName)
			{
				case "Housing":
					HousingHeight = barHeight;
					if (HousingHeight >= MaxBarHeight)
					{
						HousingHeight = 400;
					}
					break;
				case "Utilities":
					UtilitiesHeight = barHeight;
					if (UtilitiesHeight >= MaxBarHeight)
					{
						UtilitiesHeight = 400;					
					}
					break;
				case "Groceries":
					GroceriesHeight = barHeight;
					if (GroceriesHeight >= MaxBarHeight)
					{
						GroceriesHeight = 400;
					}
					break;
				case "Transportation":
					TransportationHeight = barHeight;
					if (TransportationHeight >= MaxBarHeight)
					{
						TransportationHeight = 400;
					}
					break;
				case "Entertainment":
					EntertainmentHeight = barHeight;
					if (EntertainmentHeight >= MaxBarHeight)
					{
						EntertainmentHeight = 400;
					}
					break;
				case "Miscellaneous":
					MiscHeight = barHeight;
					if (MiscHeight >= MaxBarHeight)
					{
						MiscHeight = 400;
					}
					break;
				case "Savings":
					SavingsHeight = barHeight;
					if (SavingsHeight >= MaxBarHeight)
					{
						SavingsHeight = 400;
					}
					break;
				case "Debt":
					DebtHeight = barHeight;
					if (DebtHeight >= MaxBarHeight)
					{
						DebtHeight = 400;
					}
					break;
				default:
					break;
			}
		}

		double MaxBarHeight = 400;
		public void SetGraph()
		{
			try
			{
				graphDrawViewModel.RetrieveOriginalBudgetForGraph();

				OriginalMonthlyBudget = graphDrawViewModel.OriginalMonthlyBudget;
				OriginalHousingBudget = graphDrawViewModel.OriginalHousingBudget;
				OriginalUtilitiesBudget = graphDrawViewModel.OriginalUtilitiesBudget;
				OriginalGroceriesBudget = graphDrawViewModel.OriginalGroceriesBudget;
				OriginalTransportationBudget = graphDrawViewModel.OriginalTransportationBudget;
				OriginalEntertainmentBudget = graphDrawViewModel.OriginalEntertainmentBudget;
				OriginalMiscellaneousBudget = graphDrawViewModel.OriginalMiscellaneousBudget;
				OriginalSavingsBudget = graphDrawViewModel.OriginalSavingsBudget;
				OriginalDebtBudget = graphDrawViewModel.OriginalDebtBudget;


				double maxBudgetAmount = OriginalMonthlyBudget;

				double scaleFactor = graphDrawViewModel.GraphScaleValue;

				SetHousingHeight = OriginalHousingBudget * scaleFactor;
				if (SetHousingHeight >= MaxBarHeight)
				{
					SetHousingHeight = 400;
				}
				SetUtilitiesHeight = OriginalUtilitiesBudget * scaleFactor;
				if (SetUtilitiesHeight >= MaxBarHeight)
				{
					SetUtilitiesHeight = 400;
				}
				SetGroceriesHeight = OriginalGroceriesBudget * scaleFactor;
				if (SetGroceriesHeight >= MaxBarHeight)
				{
					SetGroceriesHeight = 400;
				}
				SetTransportationHeight = OriginalTransportationBudget * scaleFactor;
				if (SetTransportationHeight >= MaxBarHeight)
				{
					SetTransportationHeight = 400;
				}
				SetEntertainmentHeight = OriginalEntertainmentBudget * scaleFactor;
				if (SetEntertainmentHeight >= MaxBarHeight)
				{
					SetEntertainmentHeight = 400;
				}
				SetMiscHeight = OriginalMiscellaneousBudget * scaleFactor;
				if (SetMiscHeight >= MaxBarHeight)
				{
					SetMiscHeight = 400;
				}
				SetSavingsHeight = OriginalSavingsBudget * scaleFactor;
				if (SetSavingsHeight >= MaxBarHeight)
				{
					SetSavingsHeight = 400;
				}
				SetDebtHeight = OriginalDebtBudget * scaleFactor;
				if (SetDebtHeight >= MaxBarHeight)
				{
					SetDebtHeight = 400;
				}

				Debug.WriteLine("Graph set");
				Debug.WriteLine("Housing: " + SetHousingHeight);
				Debug.WriteLine("Utilities: " + SetUtilitiesHeight);
				Debug.WriteLine("Groceries: " + SetGroceriesHeight);
				Debug.WriteLine("Transportation: " + SetTransportationHeight);
				Debug.WriteLine("Entertainment: " + SetEntertainmentHeight);
				Debug.WriteLine("Miscellaneous: " + SetMiscHeight);
				Debug.WriteLine("Savings: " + SetSavingsHeight);
				Debug.WriteLine("Debt: " + SetDebtHeight);

				graphDrawViewModel.RetrieveBudgetForGraph();

				Debug.WriteLine(graphDrawViewModel.RemainingMonthlyBudget);

				RemainingMonthlyBudget = graphDrawViewModel.RemainingMonthlyBudget;
				RemainingHousingBudget = graphDrawViewModel.RemainingHousingBudget;
				RemainingUtilitiesBudget = graphDrawViewModel.RemainingUtilitiesBudget;
				RemainingGroceriesBudget = graphDrawViewModel.RemainingGroceriesBudget;
				RemainingTransportationBudget = graphDrawViewModel.RemainingTransportationBudget;
				RemainingEntertainmentBudget = graphDrawViewModel.RemainingEntertainmentBudget;
				RemainingMiscellaneousBudget = graphDrawViewModel.RemainingMiscellaneousBudget;
				RemainingSavingsBudget = graphDrawViewModel.RemainingSavingsBudget;
				RemainingDebtBudget = graphDrawViewModel.RemainingDebtBudget;

				HousingHeight = RemainingMonthlyBudget * scaleFactor;
				if (HousingHeight >= MaxBarHeight)
				{
					HousingHeight = 400;
				}
				UtilitiesHeight = RemainingUtilitiesBudget * scaleFactor;
				if (SetUtilitiesHeight >= MaxBarHeight)
				{
					UtilitiesHeight = 400;
				}
				GroceriesHeight = RemainingGroceriesBudget * scaleFactor;
				if (SetGroceriesHeight >= MaxBarHeight)
				{
					SetGroceriesHeight = 400;
				}
				TransportationHeight = RemainingTransportationBudget * scaleFactor;
				if (TransportationHeight >= MaxBarHeight)
				{
					TransportationHeight = 400;
				}
				EntertainmentHeight = RemainingEntertainmentBudget * scaleFactor;
				if (EntertainmentHeight >= MaxBarHeight)
				{
					EntertainmentHeight = 400;
				}
				MiscHeight = RemainingMiscellaneousBudget * scaleFactor;
				if (MiscHeight >= MaxBarHeight)
				{
					MiscHeight = 400;
				}
				SavingsHeight = RemainingSavingsBudget * scaleFactor;
				if (SavingsHeight >= MaxBarHeight)
				{
					SavingsHeight = 400;
				}
				DebtHeight = RemainingDebtBudget * scaleFactor;
				if (DebtHeight >= MaxBarHeight)
				{
					DebtHeight = 400;
				}

				Debug.WriteLine("Graph set");
				Debug.WriteLine("Housing: " + HousingHeight);
				Debug.WriteLine("Utilities: " + UtilitiesHeight);
				Debug.WriteLine("Groceries: " + GroceriesHeight);
				Debug.WriteLine("Transportation: " + TransportationHeight);
				Debug.WriteLine("Entertainment: " + EntertainmentHeight);
				Debug.WriteLine("Miscellaneous: " + MiscHeight);
				Debug.WriteLine("Savings: " + SavingsHeight);
				Debug.WriteLine("Debt: " + DebtHeight);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error setting graph: " + ex.Message);
			}
		}

		public void ExecuteLayouts(bool isViewLayoutVisible, bool isBudgetOptionsScreenLayoutVisible, bool isAddExpenseLayoutVisible)
		{
			IsViewLayoutVisible = isViewLayoutVisible;
			IsBudgetOptionScreenVisible = isBudgetOptionsScreenLayoutVisible;
			IsAddExpenseLayoutVisible = isAddExpenseLayoutVisible;

			if(IsViewLayoutVisible)
			{
				ExecuteViewLayout();
			}
			if(IsAddExpenseLayoutVisible)
			{
				ExecuteExpenseLayout(true);
			}
			if(IsBudgetOptionScreenVisible)
			{
				IsBudgetViewVisible = false;
			}
		}

		public void ExecuteExpenseLayout(bool isAddExpenseLayoutVisible) 
		{
			IsAddExpenseLayoutVisible = isAddExpenseLayoutVisible;
			if (IsAddExpenseLayoutVisible)
			{
				IsViewLayoutVisible = false;
				IsBudgetViewVisible = false;
				IsBudgetOptionScreenVisible = false;
			}
		}

		public void ExecuteViewLayout()
		{

			IsBudgetViewVisible = true;

			SetGraph();
		}

		public double CalculateTotalBudget(Budget budget)
		{
			try
			{
				double TotalBudget = budget.HousingBudget + budget.UtilitiesBudget + budget.GroceriesBudget + budget.TransportationBudget + budget.EntertainmentBudget + budget.MiscellaneousBudget + budget.SavingsBudget + budget.DebtBudget;
				return TotalBudget;
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error calculating total budget", ex.Message);
				return 0;
			}
		}

		public Budget GetBudgetForViewModel(string userName)
		{
			Budget currentBudget = budgetRepository.ReturnUsersBudgetByName(userName);
			return currentBudget;
		}

		public void CheckAndDisplayBudget(string userName)
		{
			Budget currentBudget = GetBudgetForViewModel(userName);

			if (currentBudget != null)
			{
				ExecuteLayouts(true, false, false);
			}
			else
			{
				ExecuteLayouts(false, true, false);
			}
		}

		//Used AI to help build this method.
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

