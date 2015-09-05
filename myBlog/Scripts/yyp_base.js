var lock = false;
var page = 0;

function CastMsg(msg) {
    $(document.body).append("<div class='show_public_tips'><p class='show_public_tips_text'>" + msg + "</p></div>");
    $(".show_public_tips").fadeIn();
    setTimeout(function () {
        $(".show_public_tips").fadeOut();
        setTimeout(function () {
            $(".show_public_tips").remove();
        }, 1000);
    }, 2000);
}
function popMsg(txt) {
    var msg = $('<div class="msg hide">' + txt + '</div>');
    msg.css('left', '50%');
    $('body').append(msg);
    msg.css('margin-left', '-' + parseInt(msg.outerWidth() / 2) + 'px');
    msg.removeClass('hide');
    setTimeout(function () {
        msg.addClass('hide');
        setTimeout(function () {
            msg.remove();
        }, 400);
    }, 2600);
}

function LoadPosts() {
    if (lock) {
        return;
    }
    else {
        $("#more").text("正在加载~~~");
        lock = true;
        $.ajax({
            url: "/Home/Index",
            type: "post",
            data: { "page": page }
        }).done(function (data) {
            if (data.Statu == "end") {
                $("#more").html(data.Msg);
                return false;
            }
            if (data.Statu == "OK") {
                var str = "";
                for (var i = 0; i < data.Data.length; i++)
                {
                    var date = formatDate(data.Data[i].DateOfIssue);
                    str += "<div class='blogs'><h3><a href='/Home/PostMore?id=" + data.Data[i].ID + "'>" + data.Data[i].Title + "</a></h3><figure><img src='../images/001.jpg'></figure><ul><p>" + data.Data[i].Content + "</p><a href='/Home/PostMore?id=" + data.Data[i].ID + "'target='_blank' class='readmore'>阅读全文&gt;&gt;</a></ul><p class='autor'><span>作者:<span style='color:#199d29;margin:0px;'>" + data.Data[i].Author + "</span></span><span>分类：【<span style='color:#358080;margin:0px'>" + data.Data[i].Classify + "</span>】</span><span>浏览（<span style='color:blue;margin:0px'>" + data.Data[i].Browse + "）</span><span>评论（<span style='color:#b200ff;margin:0px;'>" + data.Data[i].ReplyCount + "</span>）</span></p><div class='dateview'>" + date + "</div></div>"
                }
                $(".bloglist").append(str);
                $("#more").html("下拉加载更多~~~");
            }
            if (data.Data.length == 10) {
                lock = false;
                page++;
            } else {
                $("#more").html("没有更多数据！");
            }

        });
    }
}

Date.prototype.format = function (format) //author: meizz
{
    var o = {
        "M+": this.getMonth() + 1, //month
        "d+": this.getDate(),    //day
        "h+": this.getHours(),   //hour
        "m+": this.getMinutes(), //minute
        "s+": this.getSeconds(), //second
        "q+": Math.floor((this.getMonth() + 3) / 3),  //quarter
        "S": this.getMilliseconds() //millisecond
    }
    if (/(y+)/.test(format)) format = format.replace(RegExp.$1,
    (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o) if (new RegExp("(" + k + ")").test(format))
        format = format.replace(RegExp.$1,
      RegExp.$1.length == 1 ? o[k] :
        ("00" + o[k]).substr(("" + o[k]).length));
    return format;
}

function formatDate(val) {
    var re = /-?\d+/;
    var m = re.exec(val);
    var d = new Date(parseInt(m[0]));
    // 按【2012-02-13 09:09:09】的格式返回日期
    return d.format("yyyy-MM-dd");
}

$(document).ready(function () {
    LoadPosts();
    $(window).scroll(
        function () {
            totalheight = parseFloat($(window).height()) + parseFloat($(window).scrollTop())
            if($(document).height() <= totalheight)
            {
                LoadPosts();
            }
    });
});
