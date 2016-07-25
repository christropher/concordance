class Concordance
  def initialize() #initialize 
  	@hash = Hash.new { Array.new } #Declare new hash with array as value
  	@i = ('a'..'zz').to_a #letter array
    puts "Enter text for Concordance:" #asks for user input
    concord = gets #gets concordance from user
    if concord #check if there is input
    	returnConcordance(concord.downcase) #calls return concordance
    end
  end
  def returnConcordance(text)
  	text = text.chomp() #removes breaks
  	text = text.split(/(?<!\w\.\w.)(?<![A-Z][a-z]\.)(?<=\.|\?)\s/) #regex for splitting into sentences
  	(text).each_with_index do |i, index| #iteration with index
  		getWords(i.chomp('.'), index+1) #removes period and passes sentence plus index
  	end
  	puts @hash.sort.map{|k,v| "#{@i.shift}.#{k}     {#{v.count}:#{v.map{|v| v.inspect}.join(',')}}"}
  	#above will map out the concordance needed, and write to the user
  end
  def getWords(word, index)
  	word = word.split(' ') #breaks words into array
  	list = (word).each_with_index.inject(@hash) do |hash, (v, i)| 
  	#above combines elements
  		k = v.sub /[?:;!,()]/, '' #formatting the key with regex
  		@hash[k] += [index] #pushing each key, value to the hash
  	end
  end
end

Concordance.new() #creating new instance of concordance
