using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.Authorize.Repository.Entity
{
    [Table("sys_menu")]
    public class Menu : BaseEntity
    {


        /// <summary>
        /// ID
        /// </summary>
        [Column(IsPrimaryKey = true, ColumnName = "menu_id", IsIdentity = true)]
        public long Id { get; set; }

        /// <summary>
        /// 菜单标题
        /// </summary>
        /// <param name=""></param>
        [Column(ColumnName = "title")]
        private string Title { get; set; }

        /// <summary>
        ///  菜单组件名称
        /// </summary>
        [Column(ColumnName = "name")]
        private string componentName { get; set; }

        /// <summary>
        ///  排序
        /// </summary>
        [Column(ColumnName = "menuSort")]
        private int menuSort = 999;

        /// <summary>
        ///  组件路径
        /// </summary>
        [Column(ColumnName = "menuSort")]
        private string component;

        @ApiModelProperty(value = "路由地址")
    private string path;

        @ApiModelProperty(value = "菜单类型，目录、菜单、按钮")
    private Integer type;

        @ApiModelProperty(value = "权限标识")
    private string permission;

        @ApiModelProperty(value = "菜单图标")
    private string icon;

        @Column(columnDefinition = "bit(1) default 0")
    @ApiModelProperty(value = "缓存")
    private Boolean cache;

        @Column(columnDefinition = "bit(1) default 0")
    @ApiModelProperty(value = "是否隐藏")
    private Boolean hidden;

        @ApiModelProperty(value = "上级菜单")
    private Long pid;

        @ApiModelProperty(value = "子节点数目", hidden = true)
    private Integer subCount = 0;

        @ApiModelProperty(value = "外链菜单")
    private Boolean iFrame;

        @Override
    public boolean equals(Object o)
        {
            if (this == o)
            {
                return true;
            }
            if (o == null || getClass() != o.getClass())
            {
                return false;
            }
            Menu menu = (Menu)o;
            return Objects.equals(id, menu.id);
        }

        @Override
    public int hashCode()
        {
            return Objects.hash(id);
        }
    }
}
