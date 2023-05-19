using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace MolarMassCalculator_MoShivKP
{
    public partial class MMC_Form : Form
    {
        public BindingSource _bs = new BindingSource();
        List<PeriodicTbClass> pt_List = new List<PeriodicTbClass>();
        public MMC_Form()
        {
            InitializeComponent();
            displayTable();
        }
        public void displayTable()
        {
            XmlDocument periodicTable = new XmlDocument();

            //https://www.data-explorer.com/data/
            //https://www.data-explorer.com/content/data/periodic-table-of-elements-xml.zip     
            string path = $@"{Path.GetFullPath(new DirectoryInfo(Environment.CurrentDirectory).FullName)}..\..\..\periodic-table-of-elements.xml";

            periodicTable.Load(path);
            
            

            UI_DGV_elementsDisplay.ColumnCount = 4;
            UI_DGV_elementsDisplay.Columns[0].Name = "Atomic #";
            UI_DGV_elementsDisplay.Columns[1].Name = "Name";
            UI_DGV_elementsDisplay.Columns[2].Name = "Symbol";
            UI_DGV_elementsDisplay.Columns[3].Name = "Mass";
            
            for (int i = 0; i < 118; i++)
            {
                int wantedField = 0; //0 for atomic number, 1 for element name, 2 for symbol, 3 for atomic weight
                string atomicNum = periodicTable.DocumentElement.ChildNodes[1].ChildNodes[i].ChildNodes[1].ChildNodes[wantedField++].InnerText;
                string name = periodicTable.DocumentElement.ChildNodes[1].ChildNodes[i].ChildNodes[1].ChildNodes[wantedField++].InnerText;
                string symbol = periodicTable.DocumentElement.ChildNodes[1].ChildNodes[i].ChildNodes[1].ChildNodes[wantedField++].InnerText;
                double mass = Math.Round(double.Parse(periodicTable.DocumentElement.ChildNodes[1].ChildNodes[i].ChildNodes[1].ChildNodes[wantedField++].InnerText), 4);
                pt_List.Add(new PeriodicTbClass(atomicNum, name, symbol, mass.ToString()));
                UI_DGV_elementsDisplay.Rows.Add(new string[] { atomicNum, name, symbol, mass.ToString()});
            }

            UI_DGV_elementsDisplay.RowHeadersVisible = false;
            
            UI_DGV_elementsDisplay.AllowUserToAddRows = false;
        } 

        private void tb_chemFormula_TextChanged(object sender, EventArgs e)
        {
            //For the group 'symbol', The first character should be capital 
            //followed by zero or atmax 2 lowercase characters
            //For the group 'atoms', the atoms entered can be none or more than zero
            string regStr = @"(?'symbol'[A-Z]{1}[a-z]{0,2})(?'atoms'\d{0,20})"; 

            Regex reg = new Regex(regStr);
            string userInput = tb_chemFormula.Text.ToString();

            if (userInput.Trim().Length > 0)
            {
                bool isAlphaNum = userInput.Any(x => (!char.IsLetterOrDigit(x)));
                if (char.IsLetter(userInput.First()) && char.IsUpper(userInput.First()) && !isAlphaNum)
                {
                    bool isMatch = reg.IsMatch(userInput);
                    Dictionary<string, ulong> chemDict = new Dictionary<string, ulong>();

                    if (reg.IsMatch(userInput))
                    {
                        MatchCollection mc = Regex.Matches(userInput, regStr);//set of successful matches
                        
                        var sym = string.Empty;
                        //var valy = 0;
                        var totalValue = 0.0;

                        //Iteration through the matched collection
                        foreach (Match gp in mc)
                        {
                            sym = gp.Groups["symbol"].ToString();

                            var col = from pl in pt_List select pl._symbol;

                            //for test like "Cssss"
                            //To make sure the molar mass is to be calculated of the symbols that exists
                            if (!col.Contains(sym))
                            {
                                tb_molarMass.BackColor = Color.Red;
                            }
                            else
                            {
                                tb_molarMass.ForeColor = Color.Green;
                                tb_molarMass.BackColor = SystemColors.Window;
                            }

                            bool newVal = ulong.TryParse(gp.Groups["atoms"].Value.ToString(), out ulong set);

                            ulong valy = 0;
                            valy = (set == 0) ? 1 : set; //if there's no number of atoms set it to one otherwise the value specified
                            
                            if (!chemDict.ContainsKey(sym))
                                chemDict.Add(sym, (ulong)valy);//initial key value for new item in dictionary
                            else
                                chemDict[sym] += (ulong)valy;//if key is already there increase the value by one
                        }

                        //Calculations:  molar mass * freq * atoms
                        var moleColl = from cd in chemDict
                                       join ptl in pt_List
                                       on cd.Key equals ptl._symbol
                                       select new 
                                       { 
                                           Element = ptl._name, 
                                           Count = cd.Value, 
                                           Mass = Math.Round(ptl._mass, 3), 
                                           TotalMass = Math.Round((cd.Value * ptl._mass), 3)
                                       };

                        foreach (var item in moleColl)
                            totalValue += item.TotalMass;

                        BindingFunction(moleColl);//used to display a refreshed version of the desired information

                        tb_molarMass.Text = $"{Math.Round(totalValue, 4)} g/mol";
                       // tb_molarMass.ForeColor = Color.Green;
                       // tb_molarMass.BackColor = SystemColors.Window;
                    }
                    else
                    {
                        Console.WriteLine("Not Matching");
                        tb_molarMass.BackColor = Color.Red;
                    }
                }
                else
                {
                   Console.WriteLine("Invalid input");
                   tb_molarMass.BackColor = Color.Red;
                }
            }
            else
            {
                RefreshUI();
                _bs.DataSource = pt_List;
                UI_DGV_elementsDisplay.DataSource = _bs;
                tb_molarMass.Text = "0 g/mol";
            }
        }

        //Helps in displaying the elements after sorting by the symbol name in ascending order
        private void bnt_sortName_Click(object sender, EventArgs e)
        {
            tb_chemFormula.Clear();
            tb_molarMass.BackColor = SystemColors.Window;

            var nameSortColl = from pt
                               in pt_List 
                               orderby pt._name 
                               select new 
                               { 
                                   AtomicNum = pt._atomicNum, 
                                   Name = pt._name, 
                                   Symbol = pt._symbol, 
                                   Mass = Math.Round(pt._mass, 4)
                               };

            BindingFunction(nameSortColl);
        }

        //Helps in displaying the elements after sorting by the atomic number in ascending order
        private void btn_atomicNo_Click(object sender, EventArgs e)
        {
            tb_chemFormula.Clear();
            tb_molarMass.BackColor = SystemColors.Window;

            var atomicSortColl = from pt 
                                 in pt_List 
                                 orderby pt._atomicNum 
                                 select new { AtomicNum = pt._atomicNum, Name = pt._name, Symbol = pt._symbol, Mass = Math.Round(pt._mass, 4)};

            BindingFunction(atomicSortColl);
        }

        //Helps in filtering and displaying the elements with single symbol
        private void btn_singleSym_Click(object sender, EventArgs e)
        {
            tb_chemFormula.Clear();
            tb_molarMass.BackColor = SystemColors.Window;

            var symSortColl = from pt
                              in pt_List
                              where pt._symbol.Length == 1
                              select new { AtomicNum = pt._atomicNum, Name = pt._name, Symbol = pt._symbol, Mass = Math.Round(pt._mass, 4) };

            BindingFunction(symSortColl);
        }

        //A function which helps in displaying the filtered or sorted values in the datagrid view
        private void BindingFunction<T>(IEnumerable<T> bindColl)
        {
            RefreshUI();
            var genericSortFilter = bindColl;
            _bs.DataSource = genericSortFilter;//binding the new collection to the binding source
            UI_DGV_elementsDisplay.DataSource = _bs;
        }

        private void RefreshUI()
        {
            //To clear everything in the datagrid view and 
            //display items using the new sorted collection
            UI_DGV_elementsDisplay.DataSource = null;
            UI_DGV_elementsDisplay.Columns.Clear();
            UI_DGV_elementsDisplay.Rows.Clear();
        }
    }
    public class PeriodicTbClass
    {
        //public automatic readonly properties
        public int _atomicNum { get; }
        public string _name { get; }
        public string _symbol { get; }
        public float _mass { get; }

        public PeriodicTbClass(string atomicNum, string name, string symbol, string mass)
        {
            _atomicNum = int.Parse(atomicNum);
            _name = name;
            _symbol = symbol;
            _mass = float.Parse(mass);
        }

    }
}
