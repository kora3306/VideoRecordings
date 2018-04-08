// ImageListView - A listview control for image files
// Copyright (C) 2009 Ozgur Ozcitak
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// Ozgur Ozcitak (ozcitak@yahoo.com)

using System;

namespace Manina.Windows.Forms
{
    #region QueuedBackgroundWorker Public Enums
    /// <summary>
    /// Represents the mode in which the work items of <see cref="QueuedBackgroundWorker"/> are processed.
    /// </summary>
    public enum ProcessingMode
    {
        /// <summary>
        /// 按照进入的顺序处理
        /// Items are processed in the order they are received.
        /// </summary>
        FIFO,
        /// <summary>
        /// 反顺寻处理
        /// Items are processed in reverse order.
        /// </summary>
        LIFO,
    }
    #endregion

    #region ImageListView Public Enums
    /// <summary>
    /// Represents the cache mode.
    /// </summary>
    public enum CacheMode
    {
        /// <summary>
        /// 项目缩略图仅在请求时才会生成
        /// Item thumbnails will be generated only when requested.
        /// </summary>
        OnDemand,
        /// <summary>
        /// 持续缓存，到达缓存限制之后停止
        /// Item thumbnails will be continuously generated. Setting
        /// the CacheMode to Continuous disables the CacheLimit.
        /// </summary>
        Continuous,
    }
    /// <summary>
    /// Represents the type of image in the cache manager.
    /// </summary>
    public enum CachedImageType
    {
        /// <summary>
        /// 缩略图图像
        /// Thumbnail image 
        /// </summary>
        Thumbnail,
        /// <summary>
        /// 小图标
        /// Small shell icon 
        /// </summary>
        SmallIcon,
        /// <summary>
        /// 大图标
        /// Large shell icon 
        /// </summary>
        LargeIcon,
    }
    /// <summary>
    /// Represents the cache state of a thumbnail image.
    /// </summary>
    public enum CacheState
    {
        /// <summary>
        /// 节点没有缓存或者还在缓存队列中
        /// The item is either not cached or it is in the cache queue.
        /// </summary>
        Unknown,
        /// <summary>
        /// 节点图片已经缓存
        /// Item thumbnail is cached.
        /// </summary>
        Cached,
        /// <summary>
        /// 已经缓存，但是缓存错误
        /// An error occurred while creating the item thumbnail.
        /// </summary>
        Error,
    }
    /// <summary>
    /// Represents the cache thread.
    /// </summary>
    public enum CacheThread
    {
        /// <summary>
        /// 缓存线程只缓存小图
        /// The cache thread responsible for generating item image thumbnails.
        /// </summary>
        Thumbnail,
        /// <summary>
        /// 缓存队列缓存所有详细信息
        /// The cache thread responsible for generating item details.
        /// </summary>
        Details,
    }
    /// <summary>
    /// 表示图像列表列的可视状态。
    /// Represents the visual state of an image list column.
    /// </summary>
    [Flags]
    public enum ColumnState
    {
        /// <summary>
        /// The column is not hovered.
        /// </summary>
        None = 0,
        /// <summary>
        /// Mouse cursor is over the column.
        /// </summary>
        Hovered = 1,
        /// <summary>
        /// Mouse cursor is over the column separator.
        /// </summary>
        SeparatorHovered = 2,
        /// <summary>
        /// Column separator is being dragged.
        /// </summary>
        SeparatorSelected = 4,
        /// <summary>
        /// The column is the sort column.
        /// </summary>
        SortColumn = 8,
    }
    /// <summary>
    /// Represents the type of information displayed in an image list view column.
    /// </summary>
    public enum ColumnType
    {
        /// <summary>
        /// A custom text column.
        /// </summary>
        Custom,
        /// <summary>
        /// The text of the item, defaults to filename if
        /// the text is not provided.
        /// </summary>
        Name,
        /// <summary>
        /// The last access date.
        /// </summary>
        DateAccessed,
        /// <summary>
        /// The creation date.
        /// </summary>
        DateCreated,
        /// <summary>
        /// The last modification date.
        /// </summary>
        DateModified,
        /// <summary>
        /// Mime type of the file.
        /// </summary>
        FileType,
        /// <summary>
        /// The full path to the file.
        /// </summary>
        FileName,
        /// <summary>
        /// The path to the folder containing the file.
        /// </summary>
        FilePath,
        /// <summary>
        /// The name of the folder containing the file.
        /// </summary>
        FolderName,
        /// <summary>
        /// The size of the file.
        /// </summary>
        FileSize,
        /// <summary>
        /// Image dimensions in pixels.
        /// </summary>
        Dimensions,
        /// <summary>
        /// Image resolution if dpi.
        /// </summary>
        Resolution,
        /// <summary>
        /// Image description (Exif tag).
        /// </summary>
        ImageDescription,
        /// <summary>
        /// The equipment model (Exif tag).
        /// </summary>
        EquipmentModel,
        /// <summary>
        /// The date image was taken (Exif tag).
        /// </summary>
        DateTaken,
        /// <summary>
        /// The artist taking the image (Exif tag).
        /// </summary>
        Artist,
        /// <summary>
        /// Image copyright information (Exif tag).
        /// </summary>
        Copyright,
        /// <summary>
        /// Exposure time in seconds (Exif tag).
        /// </summary>
        ExposureTime,
        /// <summary>
        /// The F number (Exif tag).
        /// </summary>
        FNumber,
        /// <summary>
        /// ISO speed (Exif tag).
        /// </summary>
        ISOSpeed,
        /// <summary>
        /// User comment (Exif tag).
        /// </summary>
        UserComment,
        /// <summary>
        /// Rating (Windows Exif tag).
        /// </summary>
        Rating,
        /// <summary>
        /// Software (Exif tag).
        /// </summary>
        Software,
        /// <summary>
        /// Focal length (Exif tag).
        /// </summary>
        FocalLength,
    }
    /// <summary>
    /// 表示项目绘制的顺序
    /// Represents the order by which items are drawn.
    /// </summary>
    public enum ItemDrawOrder
    {
        /// <summary>
        /// 按照索引顺序
        /// Draw order is determined by item insertion index.
        /// </summary>
        ItemIndex,
        /// <summary>
        /// 按照zorder顺序
        /// Draw order is determined by the ZOrder properties of items.
        /// </summary>
        ZOrder,
        /// <summary>
        /// 先鼠标悬停节点，在普通节点，在选择节点
        /// Hovered items are drawn first, followed by normal items and selected items.
        /// </summary>
        HoveredNormalSelected,
        /// <summary>
        /// 先鼠标悬停节点，在选中节点，在普通节点
        /// Hovered items are drawn first, followed by selected items and normal items.
        /// </summary>
        HoveredSelectedNormal,
        /// <summary>
        /// 先普通节点，在鼠标悬停节点，在选择节点
        /// Normal items are drawn first, followed by hovered items and selected items.
        /// </summary>
        NormalHoveredSelected,
        /// <summary>
        /// 先普通节点，在选择节点，在鼠标悬停节点
        /// Normal items are drawn first, followed by selected items and hovered items.
        /// </summary>
        NormalSelectedHovered,
        /// <summary>
        /// 先选中节点，在鼠标悬停节点，在普通节点
        /// Selected items are drawn first, followed by hovered items and normal items.
        /// </summary>
        SelectedHoveredNormal,
        /// <summary>
        /// 先选中节点，在普通节点，在鼠标悬停节点
        /// Selected items are drawn first, followed by normal items and hovered items.
        /// </summary>
        SelectedNormalHovered,
    }
    /// <summary>
    /// Represents the visual state of an image list view item.
    /// </summary>
    [Flags]
    public enum ItemState
    {
        /// <summary>
        /// 没有选中而且鼠标没有移动上
        /// The item is neither selected nor hovered.
        /// </summary>
        None = 0,
        /// <summary>
        /// 选中
        /// The item is selected.
        /// </summary>
        Selected = 1,
        /// <summary>
        /// 输入焦点
        /// The item has the input focus.
        /// </summary>
        Focused = 2,
        /// <summary>
        /// 鼠标移动到节点上
        /// Mouse cursor is over the item.
        /// </summary>
        Hovered = 4,
        /// <summary>
        /// 节点禁用
        /// The item is disabled.
        /// </summary>
        Disabled = 8,
    }
    /// <summary>
    /// Determines the visibility of an item.
    /// </summary>
    public enum ItemVisibility
    {
        /// <summary>
        /// 不可见
        /// The item is not visible.
        /// </summary>
        NotVisible,
        /// <summary>
        /// 部分可见
        /// The item is partially visible.
        /// </summary>
        PartiallyVisible,
        /// <summary>
        /// 全部可见
        /// The item is fully visible.
        /// </summary>
        Visible,
    }
    /// <summary>
    /// 表示嵌入的缩略图提取行为
    /// Represents the embedded thumbnail extraction behavior.
    /// </summary>
    public enum UseEmbeddedThumbnails
    {
        /// <summary>
        /// 始终从嵌入的缩略图创建缩略图
        /// Always creates the thumbnail from the embedded thumbnail.
        /// </summary>
        Always = 0,
        /// <summary>
        /// 尽可能从嵌入的缩略图创建缩略图
        /// Creates the thumbnail from the embedded thumbnail when possible,
        /// reverts to the source image otherwise.
        /// </summary>
        Auto = 1,
        /// <summary>
        /// 总是从源图像创建缩略图
        /// Always creates the thumbnail from the source image.
        /// </summary>
        Never = 2,
    }
    /// <summary>
    /// 表示Windows映像组件使用选项
    /// Represents Windows Imaging Component usage option.
    /// </summary>
    public enum UseWIC
    {
        /// <summary>
        /// 尽可能使用
        /// Uses WIC if when possible.
        /// </summary>
        Auto,
        /// <summary>
        /// 从不使用
        /// Never uses WIC.
        /// </summary>
        Never,
        /// <summary>
        /// 只有提取缩略图的时候用
        /// Uses WIC for extracting thumbnails only.
        /// </summary>
        ThumbnailsOnly,
        /// <summary>
        /// 只有读取元数据的时候用
        /// Uses WIC for reading metadata only.
        /// </summary>
        DetailsOnly,
    }
    /// <summary>
    /// Represents the view mode of the image list view.
    /// </summary>
    public enum View
    {
        /// <summary>
        /// Displays columns with image details. Thumbnail images
        /// are not displayed. The view can be scrolled vertically.
        /// </summary>
        Details,
        /// <summary>
        /// Displays a single row of thumbnails at the bottom.
        /// The view can be scrolled horizontally.
        /// </summary>
        Gallery,
        /// <summary>
        /// Displays a pane with item details.The view can be 
        /// scrolled vertically.
        /// </summary>
        Pane,
        /// <summary>
        /// Displays thumbnails laid out in a grid. The view can be 
        /// scrolled vertically.
        /// </summary>
        Thumbnails,
    }
    /// <summary>
    /// Specifies how items in a list are sorted.
    /// </summary>
    public enum SortOrder
    {
        /// <summary>
        /// 不排序
        /// The items are not sorted.
        /// </summary>
        None = 0,
        /// <summary>
        /// 升序
        /// The items are sorted in ascending order.
        /// </summary>
        Ascending = 1,
        /// <summary>
        /// 降序
        /// The items are sorted in descending order.
        /// </summary>
        Descending = 2,
        /// <summary>
        /// 自然升序 11 小于100
        /// The items are sorted in ascending natural order (ie. 11.jpg comes before 100.jpg).
        /// </summary>
        AscendingNatural = 3,
        /// <summary>
        /// 自然降序
        /// The items are sorted in descending natural order (ie. 11.jpg comes after 100.jpg).
        /// </summary>
        DescendingNatural = 4,
    }
    #endregion

    #region Internal Enums
    /// <summary>
    /// 表示鼠标选择期间的项目高亮显示状态
    /// Represents the item highlight state during mouse selection.
    /// </summary>
    public enum ItemHighlightState
    {
        /// <summary>
        /// 没有高亮
        /// Item is not highlighted.
        /// </summary>
        NotHighlighted,
        /// <summary>
        /// 删除节点高亮
        /// Item is highlighted and will be removed from the selection set.
        /// </summary>
        HighlightedAndUnSelected,
        /// <summary>
        /// 选中节点高亮
        /// Item is highlighted and will be added to the selection set.
        /// </summary>
        HighlightedAndSelected,
    }
    #endregion
}
