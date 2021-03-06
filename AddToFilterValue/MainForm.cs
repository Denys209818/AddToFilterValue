using AddToFilterValue.Entities;
using AddToFilterValue.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddToFilterValue
{
    public partial class MainForm : Form
    {
        public EFContext _context { get; set; }
        public MainForm()
        {
            InitializeComponent();
            this._context = new EFContext();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            DbSeeder.SeedAll(this._context);
            var filters = GetFilters();
            FillTreeView(filters);
            foreach (var filterName in _context.FilterNames.ToList()) 
            {
                this.cbFilter.Items.Add(filterName);
            }
        }
        private IEnumerable<FilterNameModel> GetFilters() 
        {
            List<FilterNameModel> filterNames = new List<FilterNameModel>();
            var filterName = from x in _context.FilterNames.ToList() select x;
            var filterNameValue = from x in _context.FilterNameValues.Include(x => x.FilterValue)
                                  .ToList() select x;

            List<JoinedElementsModel> joinedCollection = new List<JoinedElementsModel>();
            foreach (var name in filterName) 
            {
                List<JoinedElementsModel> el = 
                filterNameValue.Where(x => x.FilterNameId == name.Id).Select(x => new JoinedElementsModel { 
                    FilterName = name.Name, 
                    FilterNameId = name.Id,
                    FilterValue = x.FilterValue.Name,
                    FilterValueId = x.FilterValueId
                }).ToList();

                joinedCollection.AddRange(el);
            }

            var groupingColl = from x in joinedCollection
                               group x by new { x.FilterNameId, x.FilterName } into groupColl
                               select groupColl;

            foreach (var groupP in groupingColl) 
            {
                
                filterNames.Add(new FilterNameModel { 
                Id = groupP.Key.FilterNameId,
                Name = groupP.Key.FilterName,
                Children = groupP.Select(x => new FilterValueModel
                {
                    Name = x.FilterValue,
                    Id = x.FilterValueId
                }).ToArray()
                });
            }

            return filterNames;
        }
        private void FillTreeView(IEnumerable<FilterNameModel> models, int idExpand = 0) 
        {
            this.tvFilter.Nodes.Clear();
            foreach (var item in models) 
            {
                TreeNode node = new TreeNode();
                node.Name = "parent:" + item.Id.ToString();
                node.Text = item.Name;
                node.Tag = item;

                foreach (var child in item.Children) 
                {
                    TreeNode nodeChild = new TreeNode { 
                        Name = "child:" + child.Id.ToString(),
                        Text = child.Name,
                        Tag = child
                    };
                    node.Nodes.Add(nodeChild);
                }

                this.tvFilter.Nodes.Add(node);
            }

            if (idExpand != 0) 
            {
                foreach (var el in this.tvFilter.Nodes) 
                {
                    TreeNode node = el as TreeNode;
                    if ((node.Tag as FilterNameModel).Id == idExpand) 
                    {
                        node.Expand();
                    }
                }
            }
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtName.Text) && this.cbFilter.SelectedItem != null)
            {   
                FilterName element = this._context.FilterNames.SingleOrDefault(x => x.Id ==
                (this.cbFilter.SelectedItem as FilterName).Id);
                if (MessageBox.Show("Ви дійсно хочете добавити параметр?", "Програма", 
                    MessageBoxButtons.YesNo) == DialogResult.Yes) 
                {
                    if (this._context.FilterValue.SingleOrDefault(x => x.Name.ToLower() == 
                    this.txtName.Text.ToLower()) == null)
                {
                    var filterVal = new FilterValue
                    {
                        Name = this.txtName.Text
                    };
                    this._context.FilterValue.Add(filterVal);
                    this._context.SaveChanges();
                    if (this._context.FilterNameValues
                    .SingleOrDefault(x => x.FilterNameId == element.Id &&
                    x.FilterValueId == filterVal.Id) == null) 
                    {
                        this._context.FilterNameValues.Add(new FilterNameValue
                        {
                            FilterNameId = element.Id,
                            FilterValueId = filterVal.Id
                        });

                        this._context.SaveChanges();
                    }
                }
                    else 
                {
                    MessageBox.Show("Данний параметр уже існує!");
                }

                    FillTreeView(GetFilters(), element.Id); 
                }
            }
            else 
            {
                MessageBox.Show("Заповніть усі поля!");
            }
        }
    }
}
