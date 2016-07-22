class Concordance
  def initialize()
  	@hash = Hash.new { Array.new }
  	@i = 0
    puts "Enter text for Concordance:"
    concord = gets
    if concord
    	returnConcordance(concord.downcase)
    end
  end
  def returnConcordance(text)
  	text = text.chomp()
  	text = text.split(/(?<!\w\.\w.)(?<![A-Z][a-z]\.)(?<=\.|\?)\s/)
  	(text).each_with_index do |i, index|
  		getWords(i.chomp('.'), index+1)
  	end
  	puts @hash.sort.map{|k,v| "#{@i+=1}.#{k}     {#{v.count}:#{v.map{|v| v.inspect}.join(',')}}"}
  end
  def getWords(word, index)
  	word = word.split(' ')
  	list = (word).each_with_index.inject(@hash) do |hash, (v, i)|
  		k = v.sub /[?:;!,]\z/, ''
  		@hash[k] += [index]
  	end
  end
end
