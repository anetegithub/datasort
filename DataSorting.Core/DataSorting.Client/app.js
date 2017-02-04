/// <reference path="C:\Users\Anton\Source\Repos\Sorting\DataSorting.Core\DataSorting.Client\jquery/jquery-3.1.1.min.js" />
var app = {
    next: function () {
        switch (this.current) {
            case 0: this.datatype(); break;
            case 1: this.sorting(); break;
            case 2: this.load(); break;
            case 3:
                if (app.data.file == null)
                    this.current--;
                else
                    this.sendToServer();
                break;
            case 5:
                app.finally_();
                break;
            default:
                break;
        }
        this.current++;
    },
    datatype: function () {
        app.data.handler = $('select').val();
        app.html.addStage('Тип данных');
        app.html.changeDesc('Выберите тип данных (на данный момент доступно только 2):');
        app.html.pageContent($('\
                <select>\
                    <option value="0">Целое число</option>\
                    <option value="1">Число с плавающей точкой</option>\
                </select>'));
        $('select').material_select();
    },
    sorting: function () {
        app.data.type = $('select').val();
        app.html.addStage('Тип сортировки');
        app.html.changeDesc('Выберите алгоритм сортировки:');
        app.html.pageContent($('\
                <select>\
                    <option value="0">Пузырьком</option>\
                </select>'));
        $('select').material_select();
    },
    load: function () {
        app.data.sort = $('select').val();
        app.html.addStage('Загрузка файла');
        app.html.changeDesc('Загрузите ваш файл с информацией:');
        var _html = $('<input type="file" style="display:none"/><button class="btn btn-large purple upload" style="width:100%">Нажмите для выбора</button>');      
        app.html.pageContent(_html);
        $('body').find('.upload').click(function () { $('body').find('input').click(); });
        $('body').find('input').change(function (e) {         
            var file = e.target.files[0];
            var fileReader = new FileReader();
            var fileBase64 = fileReader.readAsDataURL(file);

            if (file.name.substring(file.name.length - 4).toLowerCase() == '.txt') {
                app.data.file = file;
                app.html.pageContent('<h3 class="cyan-text center">Файл успешно загружен!</h3>');
            } else
                alert('Доступна загрузка только *.txt файлов!');
            $('body').find('input').val('');
        });
    },
    sendToServer: function () {
        debugger;
        var fData = new FormData();
        fData.append('handler', app.data.handler);
        fData.append('type', app.data.type);
        fData.append('sort', app.data.sort);
        fData.append('file', app.data.file);
        
        //$(app.data).each(function (i,field) {
        //    fData.append(field, app.data[field]);
        //});

        return $.ajax({
            url: '/upload.data',
            type: 'POST',
            data: fData,
            cache: false,
            processData: false,
            contentType: false
        }).done(function (dataFromServer) {
            app.percentageLine();
        });
    },
    percentageLine: function () {
        app.html.addStage('Прогресс');
        app.html.changeDesc('Дождитесь завершения сортировки!');
        if ($('.determinate').length == 0) {
            app.html.pageContent($('<div class="progress">\
              <div class="determinate" style="width: 20%"></div>\
          </div>'));
        }
    },
    finally_:function () {
        app.html.addStage('Результат: ' + app.data.milliseconds + " ms");
        app.html.changeDesc('Результат доступен ниже:');
        var _html = $('<ul>',{class:'collection'});

        $.each(JSON.parse(app.data.end),function (i,x) {
            _html.append($('<li>',{class:'collection-item'}).html(x));
        });
        
        app.html.pageContent($('<div>').html(_html).html());
    },
    current: 0,
    html: {
        addStage: function (stageName) {
            $('.nav-wrapper .col.s12').append('<span class="breadcrumb">' + stageName + '</span>');
        },
        changeDesc: function (desc) {
            $('.helper').html(desc);
        },
        pageContent: function (jqueryObj) {
            $('.page').html(jqueryObj);
        }
    },
    data: {
        handler: -1,
        type: -1,
        sort: -1,
        file: null,
        end: null,
        milliseconds:0
    },
    progressBar:20
}

$(document).ready(function () {
    $('select').material_select();

    $.connection.notifyHub.client.percent = function () {
        
        if ($('.determinate').length == 0) {
            app.html.pageContent($('<div class="progress">\
              <div class="determinate" style="width: 20%"></div>\
          </div>'));
        }
        
        app.progressBar += 1;        
        $('.determinate').attr('style', 'width: ' + app.progressBar + '%');
    };
    $.connection.notifyHub.client.result = function (data) {
        app.data.end = data;
        app.current++;
        $('.btn.green').html('Посмотреть результат');
        $('.determinate').attr('style', 'width: 100%');
    };
    $.connection.notifyHub.client.timeSpend = function (data) {
        app.data.milliseconds = data;
    };

    $.connection.hub.start();
});